using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Threading;


namespace NASAFormApplication
{
    public partial class Form1 : Form
    {
        //Private variables
        private bool nonNumberEntered = false; // Boolean flag used to determine when a character other than a number is entered
        private int exitButtonX;
        private int exitButtonY;

        public Form1()
        {
            InitializeComponent();
        }

        ///<summary>
        ///<para> Loads form </para>
        ///</summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            //Start up message
            dynamicLabel.Text = "Welcome to our N.A.S.A. simulation! " +
                "\n\tPlease press the start button " +
                "to begin the simulation";







            foreach (Control cntrl in backgroundPanel2.Controls) // this will loop through every control contained within backgroundPanel2

            {

                if (cntrl is TextBox) // make all TextBox controls ReadOnly. You could manually go and set the ReadOnly property for every input but this is more fun

                {

                    ((TextBox)cntrl).ReadOnly = true;

                }

            }



        }



        ///<summary>

        ///<para> FILE PART OF MENU STRIP </para>

        ///</summary>

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)

        {



        }



        ///<summary>

        ///<para> EXIT PART OF MENU STRIP </para>

        ///</summary>

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)

        {

            this.Close();

        }



        ///<summary>

        ///<para> MISSION/GOAL PART OF MENU STRIP </para>

        ///</summary>

        private void goalMissionToolStripMenuItem_Click(object sender, EventArgs e)

        {

            customMessageBox("Our mission of this program is to simulate " +

                "conditions in which we can throw a supply crate to the moon" +

                " to sucessfully land using as little to no fuel at all. ");

        }



        ///<summary>

        ///<para> GROUP INFO PART OF MENU STRIP </para>

        ///</summary>

        private void groupInformationToolStripMenuItem_Click(object sender, EventArgs e)

        {
            customMessageBox("Group Members Include: " +
                "\n1. Jade Yang \n2. Kristopher Mounce " +
                "\n3. Zachary McDade \n4. Austin Abney" +
                "\n5. Ashton Brunk \n6. Colin McCormick" +
                "\n\nSpecial thanks to Mrs. Goodall, " +
                "she kept us all on track and made sure we were" +
                " on task.");
        }



        private void extraQuestionsToolStripMenuItem_Click(object sender, EventArgs e)

        {

            customMessageBox("If you have any other questions to ask," +
                " please contact us through any of the following: " +
                "\nPhone: 937-902-9873 \nEmail: zachmcdade@yahoo.com");



            //ENTER YOUR PERSONAL INFORMATION HERE (JUST FOR CONTACT)

        }



        ///<summary>

        ///<para> Start and Clear button  </para>

        ///</summary>

        private void startButton_Click(object sender, EventArgs e)

        {
            if (startButton.Text.ToUpper() == "START")

            {
                //Asking user to enter first set of information, with their name
                dynamicLabel.Text = "Welcome, please enter required information as it appear";

                //Hiding/Unhiding
                backgroundPanel.Visible = true;
                calculateButton.Visible = true;

                startButton.Text = "Clear";
                startButton.BackColor = Color.MediumBlue;

                exitButtonX = exitButton.Location.X;
                exitButtonY = exitButton.Location.Y;

                startButtonVisibile(false);

            }

            else
            {
                clearInputs();
            }
        }



        ///<summary>

        ///<para>Clears users inputs along with hiding the result section</para>

        ///</summary>

        private void clearInputs()

        {
            dropHeightBox.Text = "";
            speedBox.Text = "";
            launchAngleBox.Text = "";

            backgroundPanel2.Visible = false;
            startButtonVisibile(false);

        }

        ///<summary>

        ///<para>Hide/Show startButton along with repositioning controls</para>

        ///</summary>

        private void startButtonVisibile(bool isVisible)
        {
            if (isVisible)
            {
                if (!startButton.Visible)
                {
                    buttonPanel.Width = buttonPanel.Width + startButton.Width + 5;
                    exitButton.Location = new Point(exitButtonX, exitButtonY);

                    startButton.Visible = true;
                }
            }
            else
            {
                if (startButton.Visible)
                {
                    startButton.Visible = false;
                    exitButton.Location = startButton.Location;
                    buttonPanel.Width = exitButton.Width + 10;
                }
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            //Now ending the program
            this.Close();
        }

        ///<summary>

        ///<para>Method for returning the first character of a string to uppercase</para>

        ///</summary>

        private string MakingNameUppercase(string name) // this isn't being used
        {
            return Char.ToUpper(name.Trim()[0]) + name.Trim().ToLower().Remove(0, 1); // revised to trim the input string and make every character lower case that follows after the first
        }

        ///<summary>

        ///<para>When they click on calculate</para>

        ///</summary>

        private void calculateButton_Click(object sender, EventArgs e)

        {
            foreach (Control cntrl in backgroundPanel.Controls) // this will loop through every control contained within backgroundPanel
            {
                if (cntrl is TextBox) // handle TextBox controls only
                {

                    // perform validation of user inputs
                    TextBox inputBox = (TextBox)cntrl;
                    string test = inputBox.AccessibleName;
                    double number;



                    if (string.IsNullOrWhiteSpace(inputBox.Text)) // input can't be empty
                    {
                        customMessageBox("Error! " + ((string.IsNullOrWhiteSpace(inputBox.AccessibleName)) ? "There is an empty field!" : (inputBox.AccessibleName + " is empty!"))); // throw error message to user
                        return;
                    }

                    else if (!double.TryParse(inputBox.Text, out number)) // numbers only (can include a decimal and commas)
                    {
                        customMessageBox("Error! Enter only numbers" + ((string.IsNullOrWhiteSpace(inputBox.AccessibleName)) ? "!" : (" for " + inputBox.AccessibleName + "!"))); // throw error message to user
                        return;
                    }
                }
            }



            CalculateTotalAverageForce(double.Parse(speedBox.Text), Convert.ToInt32(dropHeightBox.Text), Convert.ToInt32(launchAngleBox.Text));

        }



        ///<summary>

        ///<para>Performs the calculation along with writing results to the UI</para>

        ///</summary>

        private void CalculateTotalAverageForce(double podSpeed, double podDropHeight, double launchAngle)
        {
            startButtonVisibile(true);

            //Variables
            double distanceOfImpact = 1.524;
            double finalVelocity = 0;
            double totalVelocity = 0;
            double totalAverageForce;
            double horizontalDisplacement;          //horizontal distance
            double time;                            //time of flight
            double initialHorizontalVelocity;              //initial horizontal velocity
            double inititalVerticalVelocity;              //initial vertical velocity
            const double MOON_GRAV_ACC = 1.62;
            double MASS_OF_POD = 3655.78;
            double launchAngleDegreesSin = Math.Sin(launchAngle / (180 / Math.PI));
            double launchAngleDegreesCos = Math.Cos(launchAngle / (180 / Math.PI));
            const double TONEWTONS = 4.44822;
            string successfulDelivery = "Success";

            // Error traps launch angle
            while (launchAngle > 9)
            {
                customMessageBox("Sorry but it must be under 10 degrees");
                return;
            }

            //Calculating time of flight for final velocity
            time = (podSpeed * launchAngleDegreesSin)
           + Math.Sqrt(Math.Pow(podSpeed * launchAngleDegreesSin, 2)
           + 2 * MOON_GRAV_ACC * podDropHeight) / MOON_GRAV_ACC;

            customMessageBox("Time = " + time);

            //Calculating final velocity from total velocity
            finalVelocity = podSpeed + (MOON_GRAV_ACC * time);

            totalVelocity = (podSpeed + finalVelocity) / 2;

            // Calculates the total average force
            totalAverageForce = MASS_OF_POD * Math.Pow(totalVelocity, 2) / (2 * distanceOfImpact);

            //Calculating initial x and y velocity
            initialHorizontalVelocity = (launchAngleDegreesCos * podSpeed);
            inititalVerticalVelocity = (launchAngleDegreesSin * podSpeed);

            //Calculating horizontal displacement
            horizontalDisplacement = initialHorizontalVelocity * time;

            //Calculating ground contact angle
            double groundContactAngle = Math.Atan((inititalVerticalVelocity / initialHorizontalVelocity)) * (180 / Math.PI);

            //Total average force to newtons
            totalAverageForce *= TONEWTONS;

            if (totalAverageForce > 3347000000)
            {
                successfulDelivery = "Failed...";
            }
            else
            {
                successfulDelivery = "Success!";
            }

            //Equations done, now display 

            //Height of Launch here
            heightOfLaunchBox.Text = podDropHeight.ToString();

            //Speed here
            speedOutputBox.Text = podSpeed.ToString();

            //Launch Angle here 
            launchAngleBoxOutputBox.Text = launchAngle.ToString();

            //Mass of Pod here
            massOfPodBox.Text = MASS_OF_POD.ToString();

            //Total average force here
            totalAverageForceBox.Text = totalAverageForce.ToString();

            //Ground contact here
            groundContactBox.Text = groundContactAngle.ToString();

            //Successful? here
            sucessfulDeliveryBox.Text = successfulDelivery.ToString();

            //Opening up the panel for results

            backgroundPanel2.Visible = true;

        }



        ///<summary>

        ///<para>Validates the KeyCode is a number only</para>

        ///</summary>

        private void numberOnly(KeyEventArgs e) // Code came from https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.control.keypress?view=netframework-4.8
        {
            nonNumberEntered = false; // Initialize the flag to false.

            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9) // Determine whether the keystroke is a number from the top of the keyboard.
            {
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9) // Determine whether the keystroke is a number from the keypad.
                {
                    if (e.KeyCode != Keys.Back) // Determine whether the keystroke is a backspace.
                    {
                        nonNumberEntered = true; // A non-numerical keystroke was pressed. Set the flag to true and evaluate in KeyPress event.
                    }
                }
            }

            if (Control.ModifierKeys == Keys.Shift) //If shift key was pressed, it's not a number.
            {
                nonNumberEntered = true;
            }
        }

        private void dropHeightBox_KeyDown(object sender, KeyEventArgs e)
        {
            numberOnly(e);
        }

        private void dropHeightBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e) // This event occurs after the KeyDown event and can be used to prevent characters from entering the control.
        {
            if (nonNumberEntered == true) // Check for the flag being set in the KeyDown event.
            {
                e.Handled = true; // Stop the character from being entered into the control since it is non-numerical.
            }
        }

        private void launchAngleBox_KeyDown(object sender, KeyEventArgs e)
        {
            numberOnly(e);
        }

        private void launchAngleBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (nonNumberEntered == true) // Check for the flag being set in the KeyDown event.
            {
                e.Handled = true; // Stop the character from being entered into the control since it is non-numerical.
            }
        }



        ///<summary>

        ///<para>Creates a new form and displays it over the current form with a custom message</para>

        ///</summary>

        private void customMessageBox(string msg)
        {
            Form myForm = new Form();
            //test.Text = "Error!";
            myForm.Height = 125;
            myForm.Width = 400;

            myForm.StartPosition = FormStartPosition.CenterParent;

            Label myLabel = new Label();
            //myLabel.Name = "errorMessage";
            myLabel.Height = 100;
            myLabel.Width = 350;
            myLabel.Location = new Point(20, 20);
            myLabel.Text = msg;

            myForm.Controls.Add(myLabel);

            myForm.ShowDialog();

        }

    }

}