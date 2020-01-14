/*
 * Created by SharpDevelop.
 * User: Peter
 * Date: 24.12.2014
 * Time: 14:34
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Speech2Keys
{
	/// <summary>
	/// Description of KeyPressedForm.
	/// </summary>
	public partial class KeyPressedForm : Form, IWorkflow
	{
		public Workflow Workflow{get;set;}
		string lastPressedKey;
		
		public KeyPressedForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
		
			
			sequenceTextBox.KeyDown += SequenceKeyDown;
			sequenceTextBox.KeyUp += SequenceKeyUp;
			sequenceListBox.KeyUp += SequenceListKeyUp;
			lastPressedKey = "none";
			
			
			/*
			string[] filler ={"none", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"
					, "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "left", "right", "up", "down", "control", "enter", "backspace", "escape"
					, "page up", "page down", "home", "end", "insert", "delete", "tab"
					, "Num1", "Num2", "Num3", "Num4", "Num5", "Num6", "Num7", "Num8", "Num9", "Num0", "+", "-", "*", "/", "execute" 
					, "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12"};
			
			*/
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void SequenceKeyUp(object sender, KeyEventArgs  e)
		{
			string pressedKey = KeyTranslator.TranslateKeyToString(e.KeyCode) + " up";	
			if (lastPressedKey != pressedKey)
			{
				lastPressedKey = pressedKey;
				sequenceListBox.Items.Add(pressedKey);		
			}
			e.Handled = true;
		}
		
		void SequenceKeyDown(object sender, KeyEventArgs  e)
		{
			string pressedKey = KeyTranslator.TranslateKeyToString(e.KeyCode) + " down";
			if (lastPressedKey != pressedKey)
			{
				lastPressedKey = pressedKey;
				sequenceListBox.Items.Add(pressedKey);		
			}
			e.Handled = true;
		}

		void SequenceListKeyUp(object sender, KeyEventArgs  e)
		{
			if (e.KeyCode == Keys.Delete)
		    {
				for (int i = sequenceListBox.SelectedIndices.Count-1; i >= 0; i--)
					if (sequenceListBox.SelectedIndices[i] >=0)
						sequenceListBox.Items.RemoveAt(sequenceListBox.SelectedIndices[i]);
				sequenceTextBox.Focus();
				sequenceListBox.ClearSelected();
			}
			sequenceTextBox.Focus();
			e.Handled = true;
		}
		
		public void Clear()
		{
			
			lastPressedKey = "none";
			sequenceListBox.Items.Clear();
			sequenceTextBox.Focus();
		
		}
		
		public void Fill(Command command)
		{
			
			
			sequenceListBox.Items.Clear();
			foreach (var s in command.sequence)
				sequenceListBox.Items.Add(s);
			sequenceTextBox.Focus();
		}
		
		public bool GetData(Command command)
		{
			int upCount = 0;
			int downCount = 0;
			foreach (var i in sequenceListBox.Items)
			{
				if (((string)i).EndsWith(" up"))
					upCount++;
				if (((string)i).EndsWith(" down"))
					downCount++;
			}
			if (upCount != downCount && (command.name != "Teamspeak on"  && command.name != "Teamspeak off"))
			{
				Workflow.parentForm.AddMessage("Number of key-up actions does not match number of key-down actions. Keys not added " + Environment.NewLine);
				return false;
			}
			command.sequence.Clear();
			foreach (var i in sequenceListBox.Items)
				command.sequence.Add((string)i);
			
			if (command.sequence.Count == 0)
				command.sequence.Add("none");
			
			return true;
		}
		
		void NextButtonClick(object sender, EventArgs e)
		{
			Workflow.NextWorkflowStep();
		}
		void CancelButtonClick(object sender, EventArgs e)
		{
			Workflow.AbortWorkflow();
		}
		void BackButtonClick(object sender, EventArgs e)
		{
			Workflow.PreviousWorkflowStep();
		}
		
		
	
		
		
		
		public void FocusOnShow()
		{
			sequenceTextBox.Focus();
			lastPressedKey = "none";
		}
		void SequenceTextBoxTextChanged(object sender, EventArgs e)
		{
			sequenceTextBox.Clear();
		}
		List<string> GetSequence()
		{
			var result = new List<string>();
			foreach (var i in sequenceListBox.Items)
				result.Add((string)i);
			return result;
		}
		void SetSequence(List<string> sequence)
		{
			sequenceListBox.Items.Clear();
			foreach (var s in sequence)
				sequenceListBox.Items.Add(s);
			sequenceTextBox.Focus();
		}
		void ClearButtonClick(object sender, EventArgs e)
		{
			sequenceListBox.Items.Clear();
			sequenceTextBox.Focus();
		}
		void PauseTenthSecondButtonClick(object sender, EventArgs e)
		{
			AddPause("Pause: 0.1 seconds");
		}
		void PauseHalfSecondButtonClick(object sender, EventArgs e)
		{
			AddPause("Pause: 0.5 seconds");
		}
		void PauseFullSecondButtonClick(object sender, EventArgs e)
		{
			AddPause("Pause: 1 second");
		}
		void PauseTwoSecondsButtonClick(object sender, EventArgs e)
		{
			AddPause("Pause: 2 seconds");
		}
		void AddPause(string pause)
		{
			var newItems = new List<string>();
			if (sequenceListBox.SelectedIndices.Count == 0)
			{
				sequenceListBox.Items.Add(pause);
			}
			else
			{
				for (int i = 0; i < sequenceListBox.Items.Count; ++i)
				{
					newItems.Add((string)(sequenceListBox.Items[i]));
					if (sequenceListBox.SelectedIndices.Contains(i))
						newItems.Add(pause);
				}
				sequenceListBox.Items.Clear();
				foreach (var n in newItems)
					sequenceListBox.Items.Add(n);
			}
			sequenceListBox.ClearSelected();
			sequenceTextBox.Focus();
		}
		
	}
}
