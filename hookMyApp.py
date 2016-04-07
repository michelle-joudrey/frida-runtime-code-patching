import frida
import os

targetProcessName = "MyApp.exe"
try:
    session = frida.attach(targetProcessName)
except Exception as ex:
    print ex
    os.system('pause')
    sys.exit(-1)
script = session.create_script("""
var module = Process.enumerateModulesSync()
    .filter(function(x) {
        return x.name == '%s';
    })[0];
var patched = false;
Process.enumerateRanges('rw-', {
    onMatch: function(range) {
        var matches = Memory.scanSync(range.base, range.size, '90 33 d2 89 55 ?? 90');
        if (matches.length == 0) {
            return;
        }
        var match = matches[0];
        Memory.writeByteArray(match.address, [ 0x31, 0xD2, 0x42 ]);
        console.log('Patched code at ' + match.address);
        patched = true;
    },
    onComplete: function() { }
});
if (!patched) {
   console.log('Failed to patch code');
}
""" % targetProcessName)
script.load()
os.system('pause')
