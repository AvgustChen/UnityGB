using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class Calculator : MonoBehaviour
{
    [SerializeField] Text _textWindow;
    double _a = 0;
    double _b = 0;
    string _one = "1";
    string _two = "2";
    string _three = "3";
    string _four = "4";
    string _five = "5";
    string _six = "6";
    string _seven = "7";
    string _eight = "8";
    string _nine = "9";
    string _zerro = "0";
    string _point = ",";
    string last = "";

    public void AC()
    {
        _textWindow.text = "0";
        _a = 0;
        _b = 0;
    }

    public void Answer()
    {
        _b = Convert.ToDouble(_textWindow.text);
        if (last == "+")
        {
            _textWindow.text = (_a + _b).ToString();
        }
        if (last == "-")
        {
            _textWindow.text = (_a - _b).ToString();
        }
        if (last == "*")
        {
            _textWindow.text = (_a * _b).ToString();
        }
        if (last == "/")
        {
            if (_b == 0) { _textWindow.text = "На 0 делить нельзя"; }
            else { _textWindow.text = (_a / _b).ToString(); }
        }

    }

    public void Plus()
    {
        last = "+";
        _a = Convert.ToDouble(_textWindow.text);
        _textWindow.text = "0";


    }
    public void Minus()
    {
        last = "-";
        _a = Convert.ToDouble(_textWindow.text);
        _textWindow.text = "0";

    }

    public void Divide()
    {
        last = "/";
        _a = Convert.ToDouble(_textWindow.text);
        _textWindow.text = "0";

    }

    public void Multiply()
    {
        last = "*";
        _a = Convert.ToDouble(_textWindow.text);
        _textWindow.text = "0";

    }

    public void Percentage()
    {
        _textWindow.text = (_a * _b / 100).ToString();

    }

    public void Point()
    {
        if (!_textWindow.text.Contains(_point)) { _textWindow.text += _point; }
    }

    public void One()
    {
        if (_b != 0) { _textWindow.text = "0"; }

        if (_textWindow.text == "0") { _textWindow.text = _one; }
        else { _textWindow.text += _one; }
    }

    public void Two()
    {
        if (_b != 0) { _textWindow.text = "0"; }

        if (_textWindow.text == "0") { _textWindow.text = _two; }
        else { _textWindow.text += _two; }
    }
    public void Three()
    {
        if (_b != 0) { _textWindow.text = "0"; }

        if (_textWindow.text == "0") { _textWindow.text = _three; }
        else { _textWindow.text += _three; }
    }
    public void Four()
    {
        if (_b != 0) { _textWindow.text = "0"; }

        if (_textWindow.text == "0") { _textWindow.text = _four; }
        else { _textWindow.text += _four; }
    }

    public void Five()
    {
        if (_b != 0) { _textWindow.text = "0"; }

        if (_textWindow.text == "0") { _textWindow.text = _five; }
        else { _textWindow.text += _five; }
    }

    public void Sis()
    {
        if (_b != 0) { _textWindow.text = "0"; }

        if (_textWindow.text == "0") { _textWindow.text = _six; }
        else { _textWindow.text += _six; }
    }

    public void Seven()
    {
        if (_b != 0) { _textWindow.text = "0"; }

        if (_textWindow.text == "0") { _textWindow.text = _seven; }
        else { _textWindow.text += _seven; }
    }

    public void Eight()
    {
        if (_b != 0) { _textWindow.text = "0"; }

        if (_textWindow.text == "0") { _textWindow.text = _eight; }
        else { _textWindow.text += _eight; }
    }

    public void Nine()
    {
        if (_b != 0) { _textWindow.text = "0"; }

        if (_textWindow.text == "0") { _textWindow.text = _nine; }
        else { _textWindow.text += _nine; }
    }

    public void Zerro()
    {
        if (_b != 0) { _textWindow.text = "0"; }

        if (_textWindow.text == "0") { _textWindow.text = _zerro; }
        else { _textWindow.text += _zerro; }
    }
}
