using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace RemoteKeyboardUi {
  public class KeyListener {
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern short GetAsyncKeyState(int nVirtKey);

    private static Dictionary<string, int> keyMap = new Dictionary<string, int> {
      // !!! must be upper case
      { "BACKSPACE", 8 },
      { "TAB", 9 },
      { "ENTER", 13 },
      { "SHIFT", 16 },
      { "CTRL", 17 },
      { "ALT", 18 },
      { "ESC", 27 },
      { "SPACE", 32 },
      { "LEFT", 37 },
      { "RIGHT", 39 },
      { "UP", 38 },
      { "DOWN", 40 },
      { "A", 65 },
      { "B", 66 },
      { "C", 67 },
      { "D", 68 },
      { "E", 69 },
      { "F", 70 },
      { "G", 71 },
      { "H", 72 },
      { "I", 73 },
      { "J", 74 },
      { "K", 75 },
      { "L", 76 },
      { "M", 77 },
      { "N", 78 },
      { "O", 79 },
      { "P", 80 },
      { "Q", 81 },
      { "R", 82 },
      { "S", 83 },
      { "T", 84 },
      { "U", 85 },
      { "V", 86 },
      { "W", 87 },
      { "X", 88 },
      { "Y", 89 },
      { "Z", 90 },
      { "F1", 112 },
      { "F2", 113 },
      { "F3", 114 },
      { "F4", 115 },
      { "F5", 116 },
      { "F6", 117 },
      { "F7", 118 },
      { "F8", 119 },
      { "F9", 120 },
      { "F10", 121 },
      { "F11", 122 },
      { "F12", 123 },
    };

    private string[] allKeys = keyMap.Keys.ToArray();
    private List<string> dirtyKeys = new List<string>();
    public readonly Dictionary<string, bool> CurrentSingleKeyStates = keyMap.Keys.ToDictionary(key => key, key => false);
    private readonly Timer _timer = new Timer(50);

    public delegate void DirtyKeysHandler(List<string> dirtyKeys);

    public event DirtyKeysHandler DirtyKeysChanged;

    public void Start() {
      _timer.Start();
    }
    
    public void Stop() {
      _timer.Start();
    }

    public KeyListener() {
      _timer.Elapsed += timer_Elapsed;
    }

    private void timer_Elapsed(object sender, ElapsedEventArgs e) => DetectBinding();

    private void DetectBinding() {
      dirtyKeys.Clear();

      foreach (string key in allKeys) {
        var state = detectKeyDown(key);

        if (state != CurrentSingleKeyStates[key]) {
          dirtyKeys.Add(key);
          CurrentSingleKeyStates[key] = state;
        }
      }

      DirtyKeysChanged.Invoke(dirtyKeys);
    }

    private int getKeyCode(string keyCode) {
      string keyCodeU = keyCode.ToUpper();
      if (keyMap.ContainsKey(keyCodeU))
        return keyMap[keyCodeU];
      else
        return -1;
    }

    public bool detectKeyDown(string keyString) {
      int keyCode = getKeyCode(keyString);
      int keyState = GetAsyncKeyState(keyCode); // keyState < 0 -> Key is pressed, it won't be > 0
      if (keyState < 0)
        return true;

      return false;
    }
  }
}