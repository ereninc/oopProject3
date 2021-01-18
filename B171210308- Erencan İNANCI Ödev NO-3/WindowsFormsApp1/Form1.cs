using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/****************************************************************************
**					      SAKARYA ÜNİVERSİTESİ
**				BİLGİSAYAR VE BİLİŞİM BİLİMLERİ FAKÜLTESİ
**				      BİLGİSAYAR MÜHENDİSLİĞİ BÖLÜMÜ
**				     NESNEYE DAYALI PROGRAMLAMA DERSİ
**					     2018-2019 BAHAR DÖNEMİ
**	
**				ÖDEV NUMARASI..........: Ödev NO-3
**				ÖĞRENCİ ADI............: Erencan İNANCI
**				ÖĞRENCİ NUMARASI.......: 
**              DERSİN ALINDIĞI GRUP...: 
****************************************************************************/

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        bool flag = true;
        bool flag2 = true;
        string test;
        string temps;
        int islem1;
        int islem2;
        int sonuc;
        char[] operands = { '/', '*', '-', '+' };
        public Form1()
        {
            InitializeComponent();
        }

        /* İşlemlerin yapıldığı fonksiyon*/
        public void hesapla(string input)
        {
            int eksiler = 0; //- (-sayı) durumunda kullanılacak değişken
            string temp; //hesaplarken işlemlerin sonucunu tutmak için değişken
            string temp2; //işlem sonucunu tutmak için 2. değişken
            int start; //string indisini almak için değişken
            int operandindex = 0; //operatörlerin indisini almak için değişken
            int end; //hesaplanacak o anki işlemin hangi indise kadar yapılacağını tutan değişken. (50/10)->0 a kadar
            for (int index = 0; index < 4;)
            {
                for (int i = 0; i < input.Length; i++) //girilen işlemin karakter uzunluğu kadar dönen döngü
                {
                    if (operands[index] == input[i])   //operatör kontrolü
                    {   // !10+5/1+6!   işlemleri daha doğru yapması için başına ve sonuna ünlem karakteri atmayı tercih ettim.
                        // 0123456789   
                        i--;
                        while (input[i] != '+' && input[i] != '-' && input[i] != '/' && input[i] != '*' && input[i]!='!') //operatör kontrolü önce bölme sonra çarpma
                        {
                            i--;
                            if (i == 0)
                            {
                                break;
                            }
                        }
                        start = ++i;
                        while (input[i] != operands[index]) //girilen işlemin i. indisi operatör olmayana kadar i yi artırıyorum.
                        {
                            i++;
                        }
                        operandindex = i++; //operatörün indisini tutan değişkene o anki i nin değerini verip  i yi 1 artırıyorum.

                        while (input[i] != '+' && input[i] != '-' && input[i] != '/' && input[i] != '*' && input[i] != '!')
                        {
                            if (++i == input.Length)
                            {
                                i--;
                                break;
                            }
                        }
                        end = --i;
                        temp = input.Substring(start, (operandindex - start)); //yapılacak işlemleri bir önceki ve bir sonraki operatöre kadar kesip geçici değişkene atan kod
                        temp2 = input.Substring(++operandindex, (end - --operandindex)); //yapılacak işlemleri bir önceki ve bir sonraki operatöre kadar kesip 2. geçici değişkene atan kod
                        islem1 = int.Parse(temp); //geçici değişkene atılan string şeklindeki işlemler integer'a dönüştürüldü. 
                        islem2 = int.Parse(temp2); //geçici değişkene atılan string şeklindeki işlemler integer'a dönüştürüldü.
                        flag2 = true;
                        if (index == 0) //bölme işlemi
                        {
                            sonuc = islem1 / islem2;
                        }
                        else if (index == 1) //çarpma işlemi
                        {
                            sonuc = islem1 * islem2;
                        }
                        else if (index == 2) //çıkarma işlemi
                        {
                            if( (islem1 - islem2)<0) //sonuç 0 dan küçükse hata veriyordu - (-sonuç) durumunda. başta tanımladığım 0 değerli değişkenden çıkartarak çözdüm.
                            {
                                eksiler += islem1 - islem2;
                                flag2 = false;
                            }
                            sonuc = islem1 - islem2; 
                        }
                        else if (index == 3) //toplama
                        {
                            sonuc = islem1 + islem2;
                        }
                        
                        /*
                         * y değerini girilen işlemin karakter sayısı kadar döndürüyor. eğer y start değerine eşitse
                         * (start değişkeninde işlemin  yapılacağı ilk karakteri tutmuştuk yani 14+5 işlem ise 1in indisi 
                         * bizim start değerimiz oluyor. operandindex değişkenimiz operatörün yerinin indisi
                         * end değerimiz ise 5in indis değeri oluyor örneğe göre) bool değişkeni false döndürüp ikinci değişkenin kontrolünü yapıyorum
                         * eğer true ise geçici değişkene o anki yapılan işlemin sonucunu ekliyorum.
                         */
                        for(int y=0;y<input.Length;y++)
                        {
                            if (y == start)
                            {
                                flag = false;
                                if (flag2)
                                    temps += sonuc;
                                else
                                    temps += 0;
                            }
                                
                            if (flag)
                                 temps += input[y].ToString();
                            if (y == end)
                                flag = true;
                        }
                        textBox1.Text = temps;
                        input = temps;
                        temps = null;
                        break;
                    }
                    if (!input.Contains(operands[index])) //en baştaki for'un değerini burada arttırıyorum. girilen işlemde o operatör yoksa değeri arttır.
                        index++;
                    if (index == 4)
                        break;
                }
            }
            sonuc = 0;
            input = input.Replace("!",""); //başta eklediğim ünlemleri çıkarttım.
            if (input == "")
                input = "0";
            sonuc = int.Parse(input) + eksiler; //en son eksi değerleri ekliyorum
            textBox2.Text = sonuc.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sayac = textBox1.Text;
            test = "!";
            test += textBox1.Text;
            test += "!";

            hesapla(test); //hesaplama işleminin gerçekleştiği fonksiyon, girdiğimiz işlem parametre ile çağırıldı.
            textBox1.Text = sayac;
        }
    }
}
