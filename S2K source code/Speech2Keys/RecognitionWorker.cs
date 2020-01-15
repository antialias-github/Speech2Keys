/*
 * Created by SharpDevelop.
 * User: Peter
 * Date: 30.11.2014
 * Time: 21:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Speech.Recognition;
using System.Windows.Forms;
using System.Threading;
using System.Speech.Synthesis;
using InputManager;
using System.Globalization;

namespace Speech2Keys
{
	/// <summary>
	/// Description of RecognitionWorker.
	/// </summary>
	public class RecognitionWorker
	{
		PhraseList phraseList;
		SpeechRecognitionEngine recognizer;
		private TextBox logTextBox;
		System.Timers.Timer timer;
		volatile Command command;
		HashSet<string> keywords;
		SpeechSynthesizer synthesizer;
		bool standby;
		ParentForm parentForm;
		CultureInfo cultureInfo = new CultureInfo("en-US");
		
		public RecognitionWorker(ParentForm _parentForm)
		{
            
			parentForm = _parentForm;
			phraseList = new PhraseList();
			synthesizer = new SpeechSynthesizer();
			synthesizer.Volume = 100; 
            synthesizer.Rate = -2;
            standby = false;
            logTextBox = parentForm.GetLogBox();
			timer = new System.Timers.Timer(1500);
            //timer.Elapsed += new ElapsedEventHandler(PhraseComplete);
            recognizer = new SpeechRecognitionEngine(cultureInfo);
			recognizer.BabbleTimeout = TimeSpan.FromSeconds(0);
			recognizer.EndSilenceTimeout = TimeSpan.FromSeconds(0);
    		recognizer.EndSilenceTimeoutAmbiguous = TimeSpan.FromSeconds(0);
 		    recognizer.InitialSilenceTimeout = TimeSpan.FromSeconds(0);			
         	recognizer.SpeechRecognized += SpeechRecognized; // if speech is recognized, call the specified method
            recognizer.RecognizeCompleted += PhraseComplete;
            try
            {
                recognizer.SetInputToDefaultAudioDevice();
            }
            catch (InvalidOperationException e)
            {
                parentForm.ErrorOnStartup();
                string bla = e.ToString();
            }
		}

		public void StartRecognition(CommandList _commandList)
		{
			standby = false;
			recognizer.RecognizeAsyncStop();
			phraseList.SetCommandList(_commandList);
			InitializeRecognition();
			recognizer.RecognizeAsync(RecognizeMode.Multiple);
            
		}
		
		public void stopRecognition()
		{
			recognizer.RecognizeAsyncStop();
		}
		
		void InitializeRecognition()
		{
			recognizer.UnloadAllGrammars();
            
			keywords = phraseList.ResetRecognition();
            foreach (var k in keywords)
            {
                GrammarBuilder gb = new GrammarBuilder
                {
                    Culture = cultureInfo
                };
                gb.Append(k);
                recognizer.LoadGrammar(new Grammar(gb));
            }
               
				//recognizer.LoadGrammar(new Grammar(new GrammarBuilder(k)));
		}
		
		void PhraseComplete(object sender, RecognizeCompletedEventArgs e)
		{
			
		}
		 
        void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
        	phraseList.RecognizePhrase(e.Result.Text);
        	command = phraseList.GetBestRecognizedCommand();
        	if (null != command)
        	{
        		if (!standby)
        		{
	        		parentForm.AddMessage("COMMAND RECOGNIZED: "+ command.name);
	        		phraseList.ResetRecognition();
	        		// catch some special commands
	        		if (command.name == "Pause Speech Recognition" || command.name == "Teamspeak on")
	        			standby = true;
	        		
	        		
	        		if (command.name == "Abort Speech Output")
	        			synthesizer.SpeakAsyncCancelAll();
	        			
	        		foreach (var s in command.sequence)
	        			PressAKey(s);

        			string text = command.GenerateResponse();
        			if (!string.IsNullOrEmpty(text) && !(command.name == "Reactivate Speech Recognition" || command.name == "Teamspeak off"))
    					synthesizer.SpeakAsync(text);
        		}
        		else
    			{
    				if (command.name == "Reactivate Speech Recognition" || command.name == "AIName" || command.name == "Teamspeak off")
    				{
    					standby = false;
    					foreach (var s in command.sequence)
	        			PressAKey(s);
    					string text = command.GenerateResponse();
    					if (!string.IsNullOrEmpty(text))
        				synthesizer.SpeakAsync(text);
    				}
    			}	
        		parentForm.SetListToName(command.name);
        	}
        }	
        
        void PressAKey(string k)
        {
        	
        	if (k.StartsWith("Pause:"))
        	{
        	    	if (k == "Pause: 0.1 seconds")
        	    		Thread.Sleep(100);
        	    	if (k == "Pause: 0.5 seconds")
        	    		Thread.Sleep(500);
        	    	if (k == "Pause: 1 second")
        	    		Thread.Sleep(1000);
        	    	if (k == "Pause: 2 seconds")
        	    		Thread.Sleep(2000);
        	    	return;
        	}
        	
        	string key = (k.Split(' '))[0];
        	string direction = (k.Split(' '))[1];
        	Keys formsKey = KeyTranslator.TranslateStringToKey(key);
        	if (direction == "up")
        		Keyboard.KeyUp(formsKey);
        	if (direction == "down")
        		
        		Keyboard.KeyDown(formsKey);
        	Thread.Sleep(40);
        }
        
        
	}
}
