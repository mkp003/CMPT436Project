using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class bluetooth : MonoBehaviour {

	private SerialPort serialPort = null;
	String portName = "/dev/tty.Bluetooth-Incoming-Port";           // ** Example BT device specs **
	int baudRate =  115200;             
	int readTimeOut = 100;                  
	int bufferSize = 32;                // Device sends 32 bytes per packet

	bool programActive = true; 
	Thread thread;

	/// <summary>
	/// Setup the virtual port connection for the BT device at program start.
	/// </summary>


	void Start () {

		try
		{
			serialPort = new SerialPort();
			serialPort.PortName = portName;

			serialPort.BaudRate = baudRate;
			serialPort.ReadTimeout = readTimeOut;   
			serialPort.Open();
			string[] ports=SerialPort.GetPortNames();
			print(serialPort.PortName);
		}
		catch (Exception e) {
			Debug.Log ("message : " + e.Message);
		}

		// Execute a thread to manage the incoming BT data
		thread = new Thread(new ThreadStart(ProcessData));
		thread.Start();
	}

	/// <summary>
	/// Processes the incoming BT data on the virtual serial port.
	/// </summary>


	void ProcessData(){
		Byte[] buffer = new Byte[bufferSize];
		int bytesRead = 0;
		Debug.Log ("Thread started");

		while (programActive) {
			try {
				print("here");
				// Attempt to read data from the BT device
				// - will throw an exception if no data is received within the timeout period
				bytesRead = serialPort.Read (buffer, 0, bufferSize);

				// Use the appropriate SerialPort read method for your BT device e.g. ReadLine(..) for newline terminated packets 
				print(bytesRead);

				if(bytesRead > 0){
					print(bytesRead);
					// Do something with the data in the buffer
				}
			} 
			catch (TimeoutException) {
				// Do nothing, the loop will be reset
			}
		}
		Debug.Log ("Thread stopped");
	}

	/// <summary>
	/// Update this instance.
	/// </summary>


	void Update () {
	}

	/// <summary>
	/// On program exit.
	/// </summary>


	public void OnDisable(){
		programActive = false;   

		if (serialPort != null && serialPort.IsOpen) 
			serialPort.Close ();
	}
}
