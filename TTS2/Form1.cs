using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Threading;

namespace TTS2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SpeechSynthesizer ssynth = new SpeechSynthesizer();
        PromptBuilder pbuilder = new PromptBuilder();
        SpeechRecognitionEngine srecognize = new SpeechRecognitionEngine();

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            pbuilder.ClearContent();
            pbuilder.AppendText(textBox1.Text);
            ssynth.Speak(pbuilder);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button3.Enabled = true;
            Choices slist = new Choices();
            slist.Add(new String[] { "Hello","How are you","Exit","Sorry","Thank you"});
            Grammar gr = new Grammar(new GrammarBuilder(slist));
            try
            {
                srecognize.RequestRecognizerUpdate();
                srecognize.LoadGrammar(gr);
                srecognize.SpeechRecognized += srecognize_SpeechRecognized;
                srecognize.SetInputToDefaultAudioDevice();
                srecognize.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch
            {
                return;
            }
        }

        void srecognize_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Text == "Exit")
            {
                Application.Exit();
            }
            else
            {
                textBox1.Text = textBox1.Text + " " + e.Result.Text.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            srecognize.RecognizeAsyncStop();
            button2.Enabled = true;
            button3.Enabled = false;
        }



    }
}
