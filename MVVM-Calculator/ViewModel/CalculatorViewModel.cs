using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Reflection.Metadata;
using System.Media;
using System.Windows.Input;
using WPFCalculator.Model;
using WPFCalculator.Interface;

namespace WPFCalculator.ViewModel
{
    public partial class CalculatorViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string result;
        [ObservableProperty]
        private string operation;
        [ObservableProperty]
        private string numberValue;
        [ObservableProperty]
        private double resultValue;

        private string[] _resultParts;

        public CalculatorViewModel()
        {
            Result = "";
            Operation = "";
            NumberValue = "";
            ResultValue = 0;
        }
        public void Add()
        {
            _resultParts = Result.Split('+');
            ResultValue = double.Parse(_resultParts[0]) + double.Parse(_resultParts[1]);
        }

        public void Remove()
        {
            _resultParts = Result.Split('-');
            ResultValue = double.Parse(_resultParts[0]) - double.Parse(_resultParts[1]);
        }

        public void Multiply()
        {
            _resultParts = Result.Split('*');
            ResultValue = double.Parse(_resultParts[0]) * double.Parse(_resultParts[1]);
        }

        public void Divide()
        {
            _resultParts = Result.Split('/');
            ResultValue = double.Parse(_resultParts[0]) / double.Parse(_resultParts[1]);
        }

        [RelayCommand]
        public void UpdateTextBlock()
        {
            SystemSounds.Beep.Play();
        }

        [RelayCommand]
        public void BuildResult(object parameter)
        {
            NumberValue = parameter as string;
            Result += char.Parse(NumberValue);
        }

        [RelayCommand]
        public void RemoveLastDigit()
        {
            Result = Result.Remove(Result.Length - 1);
        }

        [RelayCommand]
        public void UpdateOperation(object parameter)
        {
            Operation = parameter as string;
            Result += char.Parse(Operation);
        }

        [RelayCommand]
        public void CalculateResult()
        {
            switch (Operation)
            {
                case "+":
                    Add();
                    break;
                case "-":
                    Remove();
                    break;
                case "*":
                    Multiply();
                    break;
                case "/":
                    Divide();
                    break;
            }
        }
        [RelayCommand]
        public void ClearLastOperand()
        {
            Result = Result.Remove(Result.LastIndexOf(Operation));
        }
        [RelayCommand]
        public void ClearAll()
        {   
            Result = "";
            ResultValue = 0;
            Operation = "";
        }
    }
}
