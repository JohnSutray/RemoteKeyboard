using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text.Json;
using System.Windows.Forms;
using SocketIOClient;
using WindowsInput;
using WindowsInput.Native;
using Timer = System.Timers.Timer;

namespace RemoteKeyboardUi {
  public static class Logger {
    public static void Log(string message) => File.AppendAllText("./log.txt", $"{message}\n");
    
    public static void TryWithLog(Action action) {
      try {
        action();
      }
      catch (Exception e) {
        Log(e.ToString());
      }
    }
  }
  
  public class KeyboardStateEmulator {
    private Timer _timer = new Timer(50);
    private Dictionary<string, bool> _keyState = KeyboardMap.ITEMS.Keys.ToDictionary(k => k, k => false);
    private InputSimulator _simulator = new InputSimulator();
    private List<VirtualKeyCode> _dirtyKeys = new List<VirtualKeyCode>();
    private List<VirtualKeyCode> _keydownKeys = new List<VirtualKeyCode>();
    private List<VirtualKeyCode> _keyupKeys = new List<VirtualKeyCode>();
    private Dictionary<string, bool> _lastPatch = new Dictionary<string, bool>();

    public KeyboardStateEmulator() {
      _timer.Elapsed += (sender, args) => EmulationTick();
    }

    public void PatchState(Dictionary<string, bool> patch) {
      _lastPatch = patch;
    }

    public void Start() {
      _timer.Start();
    }
    
    public void Stop() {
      _timer.Stop();
    }

    private void EmulationTick() {
      Logger.TryWithLog(() => {
        if (_lastPatch == null) {
          return;
        }
        
        foreach (var patch in _lastPatch) {
          if (patch.Value && !_keyState[patch.Key]) {
            _keydownKeys.Add(KeyboardMap.TranslateKey(patch.Key));
          }

          if (!patch.Value && _keyState[patch.Key]) {
            _keyupKeys.Add(KeyboardMap.TranslateKey(patch.Key));
          }
          
          _keyState[patch.Key] = patch.Value;
        }
      
        foreach (var keyState in _keyState) {
          if (keyState.Value) {
            _dirtyKeys.Add(KeyboardMap.TranslateKey(keyState.Key));
          }
        }
        
        Logger.Log(_dirtyKeys.Count.ToString());

        if (_dirtyKeys.Any()) {
          _simulator.Keyboard.KeyPress(_dirtyKeys.ToArray());
        }
        
        _dirtyKeys.Clear();
        _keydownKeys.Clear();
        _keyupKeys.Clear();
        _lastPatch = null;
      });
    }
  }
  
  [PermissionSet(SecurityAction.Demand, Name = "SkipVerification")]
  public partial class Form1 : Form {
    private SocketIO _socket = new SocketIO("http://18.197.33.127:3000");
    private KeyListener _sendModeListener = new KeyListener();
    private KeyboardStateEmulator _keyboardStateEmulator = new KeyboardStateEmulator();

    private bool InReceiveMode => ReceiveModeRadioButton.Checked;
    private bool InSendMode => SendModeRadioButton.Checked;
    private string CurrentRoomId => RoomIdTextbox.Text;

    private void InitializeCheckers() {
      _sendModeListener.DirtyKeysChanged += SendKeyboard;
    }

    private void SendKeyboard(List<string> dirtyKeys) {
      if (!InSendMode || dirtyKeys.Count == 0) {
        return;
      }

      var keys = new Dictionary<string, bool>();

      foreach (var dirtyItem in dirtyKeys) {
        keys[dirtyItem] = _sendModeListener.CurrentSingleKeyStates[dirtyItem];
      }

      _socket.EmitAsync("keyboard", new {
        id = RoomIdTextbox.Text,
        value = keys
      });
    }

    private void CheckRoomIdValidity() {
      ModeGroupBox.Enabled = RoomIdTextbox.Text.Length > 2;
    }

    public Form1() {
      InitializeComponent();
      InitializeCheckers();
      InitializeConnection();
      InitializeReceiver();
      EnableSetupControls();
      CheckRoomIdValidity();
      
      RoomIdTextbox.TextChanged += (sender, args) => CheckRoomIdValidity();
    }

    private void ReceiveKeys(SocketIOResponse response) {
      var keysState = response.GetValue(1).Deserialize<Dictionary<string, bool>>();

      _keyboardStateEmulator.PatchState(keysState);
    }

    private void InitializeReceiver() {
      _socket.OnAny((message, response) => {
          if (LogTextBox.InvokeRequired) {
            LogTextBox.Invoke(new Action(() => ReceiveKeys(response)));
          }

          // LogTextBox.Text += string.Join(", ", response.GetValue(1).Deserialize<Dictionary<string, string>>().Keys) + "\n";
        }
      );
    }

    private void InitializeConnection() {
      _socket.ConnectAsync();
    }

    private void ReceiveModeRadioButton_CheckedChanged(object sender, EventArgs e) {
      if (InReceiveMode) {
        StartReceiver();
      }
      else {
        StopReceiver();
      }
      
      DisableSetupControls();
    }

    private void StartReceiver() {
      _socket.EmitAsync("receiver", new { id = CurrentRoomId });
      _keyboardStateEmulator.Start();
    }

    private void StopReceiver() {
      _socket.EmitAsync("receiver-stop", new { id = CurrentRoomId });
      _keyboardStateEmulator.Stop();
    }

    private void SendModeRadioButton_CheckedChanged(object sender, EventArgs e) {
      if (SendModeRadioButton.Checked) {
        _sendModeListener.Start();
      }
      else {
        _sendModeListener.Stop();
      }
      
      DisableSetupControls();
    }

    private void DisableSetupControls() {
      StopButton.Enabled = true;
      RoomIdTextbox.Enabled = false;
      ModeGroupBox.Enabled = false;
    }

    private void StopButton_Click(object sender, EventArgs e) {
      if (InReceiveMode) {
        StopReceiver();
      }
      
      EnableSetupControls();
    }

    private void EnableSetupControls() {
      StopButton.Enabled = false;
      RoomIdTextbox.Enabled = true;
      ModeGroupBox.Enabled = true;
    }
  }
  
}