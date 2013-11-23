using System;
using System.Drawing;

namespace CRC.Controls
{
	internal class  ColorTransform
	{
        // Color transform will transform a color from a start color to a goal color through a number of steps.
        // Example:
        // ColorTransform colorTransform = new ColorTransform(Color.Red, Color.Blue, 10);
        // while (colorTransform.Transform())
        // {
        //     Color color = colorTransform.Color;
        //     Console.WriteLine(color.ToString());
        // }
        //
        // The output will be the following, as we cans see the Red component fades away while the Blue component 
        // gets more solid for each step.
        //
        // Color [A=255, R=230, G=0, B=25]
        // Color [A=255, R=204, G=0, B=51]
        // Color [A=255, R=179, G=0, B=76]
        // ...
        // Color [A=255, R=26, G=0, B=229]
        // Color [A=255, R=0, G=0, B=255]

        private Color start;
        private Color goal;
        private int steps;
        private int currentStep = 0;

		public ColorTransform(Color start, Color goal, int steps)
		{
            this.start = start;
            this.goal = goal;
            this.steps = steps;
		}

        public bool Transform()
        {
            // Returns true if we are not done, that is more transformations are
            // necessary before goal color is reached. 
            // Returns false when color is same as goal color,

            if (currentStep < steps)
            {
                currentStep++;
                return true;
            }
            else
            {
                return false;
            }
        }

        public Color Color
        {
            // Get current transformed color.

            get
            {
                int red = start.R + currentStep * (goal.R - start.R) / steps;
                int green = start.G + currentStep * (goal.G - start.G) / steps;
                int blue = start.B + currentStep * (goal.B - start.B) / steps;
                
                return Color.FromArgb(red, green, blue);
            }
        }
	}
}
