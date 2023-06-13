Repro of regression? from net7 to net8p4

Run on a physical device, need to sort out codesigning etc. 

Works:
./net7.sh {Your Device UDID}

Crashes:
./net8.sh {Your Device UDID}

In AppDelegate.cs, there are some attempts to isolate the problematic aspect. All commented-out lines work on both net7 and net8p4 