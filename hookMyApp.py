import frida
import os

targetProcessName = "MyApp.exe"
session = frida.attach(targetProcessName)
script = session.create_script("""
var targetProcessName = 'MyApp.exe';
var module = Process.enumerateModulesSync()
    .filter(function(x) {
        return x.name == targetProcessName;
    })[0];
Process.enumerateRanges('rw-', {
    onMatch: function(range) {
        var matches = Memory.scanSync(range.base, range.size, '90 33 d2 89 55 ?? 90');
        if (matches.length == 0) {
            return;
        }
        if (matches.length > 1) {
            console.log('Something has gone terribly wrong');
            return;
        }
        var match = matches[0];
        console.log('Found pattern at ' + match.address);
        Memory.writeByteArray(match.address, [0x31, 0xD2, 0x42]);
        matches = Memory.scanSync(range.base, range.size, '31 d2 42 89 55 ?? 90');
        if (matches.length == 0) {
            console.log('Failed to write memory');
            return;
        }
        console.log('Succesfully wrote memory');
    },
    onComplete: function() {
        console.log('complete');
    }
});
""")
script.load()
os.system('pause')
