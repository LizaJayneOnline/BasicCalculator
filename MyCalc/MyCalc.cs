namespace MyCalc
{
    public partial class MyCalc : Form
    {
        // set up decimal checker and equals checker before main initialization even occurs
        bool decimalFlag = false;
        bool totalFlag = false;
        bool zeroFlag = false; // prevents a number after an operator beginning with 0

        // set up Queue to contain the OPERATIONS (numbers and then operators (SO 1+1 is 3 operations) )
        Queue<string> Calcs = new Queue<string>();
        string NumString = ""; // set an empty value to catch
                               // if an operator is clicked first (sets first number to 0)


        double total = 0;  // "total" starts at 0 to correctly display total after = is clicked

        public MyCalc()
        {
            InitializeComponent();
        }
        
        private void button_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;  // takes the "Text" value of the button clicked and assigns it to variable b
            string inputTxt = b.Text; // takes the value of variable b and turns it into a string called inputTxt - used later in switch statement to code input

            // if a total has been calculated, refresh the display
            if (totalFlag == true)
            {
                output.Text = "0";
                totalFlag = false;
            }

            // if 0 in output, and a number is typed - clear the window for the number typed
            // but not if an operator typed first - do not clear output but use 0 as the firt "OPERATION"
            if (output.Text == "0"
                && !inputTxt.Contains ("*") && !inputTxt.Contains("/") && !inputTxt.Contains("+") && !inputTxt.Contains("-"))
            {
                output.Text = "";
            }
            // if output Text is "0" and the input Text IS NOT * , / , + , -
            // CLEAR output window (output.Text = ""; (empty characters)


            switch (inputTxt) // setting your output text (display) to add to the number until an operator is clicked (instead of changing with each new character clicked) 
            {
                case "0":
                    if (zeroFlag == false)
                    {
                        return;
                    }
                    else
                    {
                        output.Text += "0";
                        NumString += "0";
                    }
                    break;
                    

                case "1":
                    output.Text += "1";
                    NumString += "1";
                    zeroFlag = true;
                    break;

                case "2":
                    output.Text += "2";
                    NumString += "2";
                    zeroFlag = true;
                    break;

                case "3":
                    output.Text += "3";
                    NumString += "3";
                    zeroFlag = true;
                    break;

                case "4":
                    output.Text += "4";
                    NumString += "4";
                    zeroFlag = true;
                    break;

                case "5":
                    output.Text += "5";
                    NumString += "5";
                    zeroFlag = true;
                    break;

                case "6":
                    output.Text += "6";
                    NumString += "6";
                    zeroFlag = true;
                    break;

                case "7":
                    output.Text += "7";
                    NumString += "7";
                    zeroFlag = true;
                    break;

                case "8":
                    output.Text += "8";
                    NumString += "8";
                    zeroFlag = true;
                    break;

                case "9":
                    output.Text += "9";
                    NumString += "9";
                    zeroFlag = true;
                    break;

                    // SPECIAL CONSIDERATIONS FOR OPERATORS AND DECIMAL

                case ".":  // only single decimal allowed per operation (numbers between operators)
                    if (decimalFlag == false) // if no decimal detected
                    {
                        decimalFlag = true; // switch flag to true to prevent another decimal
                        zeroFlag = true;

                        if (NumString.Length == 0) // if less than 1, add a 0 to beginning before decimal
                        {
                            output.Text += "0.";
                            zeroFlag = true;
                        }
                        else // if another number besides 0 already selected, add decimal to exisiting number
                        {
                            output.Text += ".";
                            zeroFlag = true;
                        }
                        NumString += ".";
                    }
                    break;

                case "*":
                    if (NumString.Length > 0)  // operator will only do something if a number has been selected first
                    {
                        Calcs.Enqueue(NumString); // take everything typed so far and store it in the "Calcs" Queue (created before Initialization)
                        output.Text += " * ";
                        Calcs.Enqueue("*"); // also add the operator itself into the Calcs Queue
                        NumString = ""; // set numstring back to empty ready for next number to be typed
                        decimalFlag = false; // set decimal flag back to false as a new number is starting
                        zeroFlag = false;
                    }
                    break;

                case "/":
                    if (NumString.Length > 0)  // operator will only do something if a number has been selected first
                    {
                        Calcs.Enqueue(NumString); // take everything typed so far and store it in the "Calcs" Queue (created before Initialization)
                        output.Text += " / ";
                        Calcs.Enqueue("/"); // also add the operator itself into the Calcs Queue
                        NumString = ""; // set numstring back to empty ready for next number to be typed
                        decimalFlag = false; // set decimal flag back to false as a new number is starting
                        zeroFlag = false;
                    }
                    break;

                case "+":
                    if (NumString.Length > 0)  
                    {
                        Calcs.Enqueue(NumString); 
                        output.Text += " + ";
                        Calcs.Enqueue("+"); 
                        NumString = ""; 
                        decimalFlag = false;
                        zeroFlag = false;
                    }
                    break;

                case "-":
                    if (NumString.Length > 0)  
                    {
                        Calcs.Enqueue(NumString);
                        output.Text += " - ";
                        Calcs.Enqueue("-"); 
                        NumString = ""; 
                        decimalFlag = false;
                        zeroFlag = false;
                    }
                    break;

                // THE EQUALS BUTTON

                case "=":
                    if (NumString != "") // if numstring DOES NOT equal an empty value
                    {
                        Calcs.Enqueue(NumString);  // send the numstring to the calcs queue
                    }
                    if (Calcs.Count < 3 || NumString == "") // if there is less than 3 operators (minimum full opertation needs two numbers and an operator) OR NumString is empty
                    { 
                       return; // do nothing
                        
                    }
                    else
                    {
                        NumString = "";  // reset numstring and decimal flag
                        decimalFlag = false;
                        output.Text += " = "; // add = to the display 
                        computeTotal(); // run computeTotal method - coded below
                    }
                    break;

                    // THE CLEAR BUTTON

                case "C":
                    decimalFlag = false;  // reset decimal flag
                    totalFlag = true; // total flag to true as if a total had been reached
                    zeroFlag = false;
                    Calcs.Clear(); // clear the calcs queue of all
                    NumString = ""; // reset numstring
                    output.Text = "0"; // display back to 0
                    total = 0;
                    break;

                    default:
                    return;
            }
        }

        public void computeTotal()
        {
            string[] calcString = new string[Calcs.Count]; // new string array length set to the length of the Calcs Queue
            Calcs.CopyTo(calcString, 0); // convert the Calcs Queue into a string
            double val1 = 0;
            double val2 = 0;  // use 2 values to capture every value before and after an operator

            bool FirstOp = true; // first op will total differently, all subsequents ops will compound from result before

            // proccess the string
            for (int i = 0; i < Calcs.Count; i++)
            {
                switch (calcString[i])
                {
                    case "*":
                        val1 = Convert.ToDouble(calcString[i-1]);  // find the item at -1 of the * (the number before the *)
                        val2 = Convert.ToDouble(calcString[i+1]);  // find the item at +1 of the * (the number after the *)
                        if (FirstOp == true) // if first operation
                        {
                            total = val1 * val2;
                            FirstOp = false;
                        } 
                        else  // if NOT first operation
                        {
                            total = total * val2;
                        }
                        break;

                    case "/":
                        val1 = Convert.ToDouble(calcString[i - 1]);  
                        val2 = Convert.ToDouble(calcString[i + 1]);  
                        if (FirstOp == true) 
                        {
                            total = val1 / val2;
                            FirstOp = false;
                        }
                        else  
                        {
                            total = total / val2;
                        }
                        break;

                    case "+":
                        val1 = Convert.ToDouble(calcString[i - 1]);
                        val2 = Convert.ToDouble(calcString[i + 1]);
                        if (FirstOp == true)
                        {
                            total = val1 + val2;
                            FirstOp = false;
                        }
                        else
                        {
                            total = total + val2;
                        }
                        break;

                    case "-":
                        val1 = Convert.ToDouble(calcString[i - 1]);
                        val2 = Convert.ToDouble(calcString[i + 1]);
                        if (FirstOp == true)
                        {
                            total = val1 - val2;
                            FirstOp = false;
                        }
                        else
                        {
                            total = total - val2;
                        }
                        break;

                    default:
                        break;
                }
            }

            // OUTPUT THE TOTAL

            output.Text += "" + total;
            totalFlag = true;
            Calcs.Clear();  // reset calcs queue
            Array.Clear(calcString, 0, calcString.Length); // reset array
            total = 0; // reset total for next click
        }
    }
}