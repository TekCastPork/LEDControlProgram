using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Xceed.Wpf.Toolkit;

namespace WpfApp1
{
    /// <summary>
    /// Framework for a Frame of LED animation
    /// </summary>    
    public class Frame
    {
        int duration;
        int ID;
        Color[] colorMap = new Color[60];
        public Frame(int duration, int ID, Color[] map)
        {
            this.colorMap = map;
            this.ID = ID;
            this.duration = duration;
        }
    }    

    public partial class MainWindow : Window
    {
        List<Frame> frameList = new List<Frame>(); // List to contain created frames for LEDs
        Rectangle selectedRect;
        int selectedRectIndex = 0;
        static Color[] currentMap = new Color[60];
        Frame currentFrame = new Frame(0, 0, currentMap);
        public MainWindow()
        {
            for (int u = 0; u < 20; u++) {
                Color[] storeMap = new Color[60];
                for (int i = 0; i < 60; i++) {
                    storeMap[i] = Color.FromRgb((byte)i, (byte)(255-i), 0);
                }
                Frame storeFrame = new Frame(u+50, u, storeMap);
                frameList.Add(storeFrame);
            }
            
            InitializeComponent();
            
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle sendingRect = sender as Rectangle;
            Console.WriteLine("MouseDown Event on object:  {0}", sendingRect.Name);
            sendingRect.Opacity = 0.4;
        }

        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            Rectangle sendingRect = sender as Rectangle;
            Console.WriteLine("MouseEnter Event on object:  {0}", sendingRect.Name);
        }

        private void Rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            Rectangle sendingRect = sender as Rectangle;
            Console.WriteLine("MouseLeave Event on object: {0}",sendingRect.Name);
        }

        private void Rectangle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Rectangle sendingRect = sender as Rectangle;
            Console.WriteLine("MouseUp Event on object: {0}", sendingRect.Name);
            sendingRect.Opacity = 1;
            colorChooser.SelectedColor = ((SolidColorBrush)sendingRect.Fill).Color;
            selectedRect = sendingRect;
            string reducedName = sendingRect.Name.Replace("led", "");
            selectedRectIndex = int.Parse(reducedName);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button sendingButton = sender as Button;
            Console.WriteLine("Click Event on object: {0}", sendingButton.Name);
            if(sendingButton.Name.Equals("applyBut"))
            {
                Console.WriteLine("Apply Button pushed");
                selectedRect.Fill = new SolidColorBrush(Color.FromRgb(colorChooser.R, colorChooser.G, colorChooser.B));
                currentMap[selectedRectIndex-1] = Color.FromRgb(colorChooser.R, colorChooser.G, colorChooser.B);
            } else if(sendingButton.Name.Equals("saveFrameBut"))
            {
                frameList.RemoveAt(int.Parse(frameIDBox.Text));
                frameList.Insert(int.Parse(frameIDBox.Text), currentFrame);
                Console.WriteLine("Frame saved to location {0}", int.Parse(frameIDBox.Text));
            } else if(sendingButton.Name.Equals("getFrameBut"))
            {
                Console.WriteLine("Get Frame Button!");
                int frameID = int.Parse(frameIDBox.Text); // get frame ID
                if(frameID >= frameList.Count)
                {
                    //Too big
                } else
                {
                    currentFrame = frameList.ElementAt(frameID);
                }
            }
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox sendingBox = sender as TextBox;
            Console.WriteLine("TextChanged Event on object: {0}", sendingBox.Name);
        }

    }
}
