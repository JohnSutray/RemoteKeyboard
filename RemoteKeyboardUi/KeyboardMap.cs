using System.Collections.Generic;
using WindowsInput.Native;

namespace RemoteKeyboardUi {
  public class KeyboardMap {
    public static Dictionary<string, VirtualKeyCode> ITEMS = new Dictionary<string, VirtualKeyCode> {
      ["A"] = VirtualKeyCode.VK_A,
    };
  }
}