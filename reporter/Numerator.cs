using System;
using System.Collections.Generic;
using System.Text;

namespace reporter
{
    class Numerator
    {
        int summa;
        
       
        public string GetText(string text)
        {
            string text_nums = "";
            if(text != "")
                summa = Convert.ToInt32(text);
            text_nums = numbers9999(summa);
            if (text_nums.IndexOf("один тысяч") != -1)
                text_nums = text_nums.Replace("один тысяч", "одна тысяча");
            else if (text_nums.IndexOf("два тысяч") != -1)
                text_nums = text_nums.Replace("два тысяч", "две тысячи");
            else if (text_nums.IndexOf("три тысяч") != -1)
                text_nums = text_nums.Replace("три тысяч", "три тысячи");
            else if (text_nums.IndexOf("четыре тысяч") != -1)
                text_nums = text_nums.Replace("четыре тысяч", "четыре тысячи");
            if (text_nums.Length > 0)
            {
                string str = text_nums.Substring(0, 1);
                text_nums = str.ToUpper() + text_nums.Remove(0, 1);
            }

            return text_nums;
        }

        private string numbers99(int f)        
        {            
            if (f < 10)
            {                
                return fNumbs(f);
            }
            else if ((f > 10) & (f < 20))
            {                
                return fNumbs11(f); 
            }
            else if (((f > 19) & (f < 100)) || (f == 10))
            {               
                return (fNumbs20((f / 10) * 10) + " " + fNumbs(f - (f / 10) * 10));
            }
            else 
                return "";
        }

        private string numbers999(int f)
        {
            string number_text = "";
            if (f < 100)
            {
                number_text = numbers99(f);
                return number_text;
            }
            else if ((f >= 100) & (f < 1000))
            {
                number_text = fNumbs100((f / 100) * 100) + " " + numbers99(f - (f / 100) * 100);
                return number_text;
            }           
            else
                return number_text;
        }

        private string numbers9999(int f)
        {
            string number_text = "";
            if (f < 1000)
            {
                number_text = numbers999(f);
                return number_text;
            }
            else if ((f > 999) & (f < 1000000))
            {
                number_text = numbers999((f / 1000)) +  " тысяч " + numbers999(f - (f / 1000) * 1000);;
                return number_text;
            }
            else
                return number_text;
        }
        
        private string fNumbs(int f)
        {            
            switch (f)
            {                
                case 1:                     
                    return "один";                    
                case 2:                    
                    return "два"; 
                case 3:
                    return "три"; 
                case 4:                   
                    return "четыре"; 
                case 5:                    
                    return "пять"; 
                case 6:                    
                    return "шесть"; 
                case 7:                    
                    return "семь"; 
                case 8:                    
                    return "восемь";
                case 9:                    
                    return "девять";
                default:
                    return "";
            }
        }

        private string fNumbs11(int f)
        {
            switch (f)
            {
                case 11:
                    return "одинадцать";                    
                case 12:
                    return "двенадцать";                    
                case 13:
                    return "тринадцать";                    
                case 14:
                    return "четырнадцать";                    
                case 15:
                    return "пятнадцать";                    
                case 16:
                    return "шестнадцать";                    
                case 17:
                    return "семнадцать";                    
                case 18:
                    return "восемнадцать";                    
                case 19:
                    return "девятнадцать";
                default:
                    return "";
            }
        }

        private string fNumbs20(int f)
            {
                switch (f)
                {
                    case 10:
                       return "десять";                        
                    case 20:
                       return "двадцать";                        
                    case 30:
                       return "тридцать";                        
                    case 40:
                       return "сорок";                        
                    case 50:
                       return "пятьдесят";                        
                    case 60:
                       return "шестьдесят";                       
                    case 70:
                       return "семьдесят";                        
                    case 80:
                       return "восемьдесят";                       
                    case 90:
                       return "девяносто";
                    default:
                       return "";
                }



        }

        private string fNumbs100(int f)
            {
                switch (f)
                {
                    case 100:
                       return "сто";                        
                    case 200:
                       return "двести";                        
                    case 300:
                       return "триста";                        
                    case 400:
                       return "четыреста";                        
                    case 500:
                       return "пятьсот";                        
                    case 600:
                       return "шестьсот";                       
                    case 700:
                       return "семьсотт";                        
                    case 800:
                       return "восемьсот";                       
                    case 900:
                       return "девятьсот";
                    default:
                       return "";
                }



        }  
       
    }
}
