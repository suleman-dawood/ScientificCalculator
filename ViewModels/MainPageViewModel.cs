using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScientificCalculator.ViewModels
{
    // Attribute to automatically implement INotifyPropertyChanged
    [INotifyPropertyChanged]
    internal partial class MainPageViewModel
    {
        // Observable property for user input text
        [ObservableProperty]
        private string inputText = "";

        [ObservableProperty]
        private string resultText = "0";

        // Flag to check if waiting for an operator
        private bool isWaiting = false;

        public MainPageViewModel() { }

        [RelayCommand]
        private void Reset()
        {
            ResultText = "0";
            InputText = "";
            isWaiting = false;
        }

        [RelayCommand]
        private void Calculate()
        {
            if (InputText.Length == 0)
                return;

            // Close any open parenthesis if necessary
            if (isWaiting)
            {
                InputText += ")";
                isWaiting = false;
            }

            try
            {
                var inputString = NormalizeInput();
                var expression = new Expression(inputString);
                expression.EvaluateParameter += delegate (string name, ParameterArgs args)
                {
                    if (name == "Pi")
                        args.Result = Math.PI;
                };
                expression.EvaluateParameter += delegate (string name, ParameterArgs args)
                {
                    if (name == "e")
                        args.Result = Math.Exp(1);
                };
                var result = expression.Evaluate();

                ResultText = result.ToString();
            }
            catch (Exception ex)
            {
                ResultText = "NaN";
            }
        }

        private string NormalizeInput()
        {
            Dictionary<string, string> Mapper = new()
            {
                {"×", "*"},
                {"÷", "/"},
                {"LN", "Ln"},
                {"ASIN", "Asin"},
                {"ACOS", "Acos"},
                {"ATAN", "Atan"},
                {"SIN", "Sin"},
                {"COS", "Cos"},
                {"TAN", "Tan"},
                {"LOG", "Log"},
                {"EXP", "Exp"},
                {"LOG10", "Log10"},
                {"ROUND", "Round"},
                {"π", "Pi"},
                {"POW", "Pow"},
                {"SQRT", "Sqrt"},
                {"ABS", "Abs"},
            };

            // Replace custom operators with standard operators in the input text
            var newString = InputText;

            foreach (var key in Mapper.Keys)
            {
                newString = newString.Replace(key, Mapper[key]);
            }

            return newString;
        }

        [RelayCommand]
        private void Backspace()
        {
            if (InputText.Length > 0)
                InputText = InputText.Substring(0, InputText.Length - 1);
        }

        [RelayCommand]
        private void NumberInput(string key)
        {
            InputText += key;
        }

        // Command to append a scientific operator and set waiting flag
        [RelayCommand]
        private void ScientificOperator(string operation)
        {
            InputText += $"{operation}(";
            isWaiting = true;
        }

        // Command to append a region operator, and handle closing parenthesis
        [RelayCommand]
        private void RegionOperator(string operation)
        {
            if (operation == ")")
                isWaiting = false;

            InputText += operation;
        }

        // Command to append a math operator, and handle any open parenthesis
        [RelayCommand]
        private void MathOperator(string op)
        {
            if (isWaiting)
            {
                InputText += ")";
                isWaiting = false;
            }

            InputText += $" {op} ";
        }
    }
}
