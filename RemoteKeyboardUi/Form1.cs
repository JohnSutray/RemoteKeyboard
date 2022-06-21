using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;
using com.jarvisniu.utils;
using SocketIOClient;
using WindowsInput;
using WindowsInput.Native;

namespace RemoteKeyboardUi {

  [PermissionSet(SecurityAction.Demand, Name = "SkipVerification")]
  public partial class Form1 : Form {
    private SocketIO socketIo = new SocketIO("http://3.73.158.78:3000");
    private KeyListener _listener = new KeyListener();
    private InputSimulator _simulator = new InputSimulator();

    private void initializeHost(string id) {
      _simulator.Keyboard.KeyPress(VirtualKeyCode.VK_B);
    }

    public Form1() {
      InitializeComponent();

      foreach (var itemsKey in KeyboardMap.ITEMS.Keys) {
        _listener.onPress(itemsKey, () => {
          textBox1.Text += "A";
        });

        _listener.onRelease(itemsKey, () => {

        });
      }

      socketIo.OnAny((type, response) => {
          try {
            Console.WriteLine(
              type
            );

            Console.WriteLine(response.GetValue(1));
          }
          catch (Exception e) {
            Console.WriteLine(e);
            throw;
          }
        }
      );

      socketIo.OnError += (sender, s) => { Console.WriteLine(s); };

      // socketIo.ConnectAsync().GetAwaiter().GetResult();
      //
      // socketIo.EmitAsync("receiver", new { id = "1" }).GetAwaiter().GetResult();
      //
      // socketIo.EmitAsync("keyboard", new { id = "1", value = new { a = 1 } }).GetAwaiter().GetResult();

      Console.ReadLine();
    }
  }
}