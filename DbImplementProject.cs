/////////////////////             READ ME                 ////////////////////////

                           **ساختار الگوریتم  پروژه**
این پروژه با روی کرد الگوریتم برنامه نویسی پویا به این صورت پیاده سازی شده است که
مجموعه رابطه های دریافتی از کاربر در لیستی از جنس کلاس رابطه زخیره میگردد و با هربار
فراخوانی تابع محاسبه هزینه این لیست به روز رسانی خواهد شد به این صورت که در مرحله 
نخست جایی که از کاربر مقادیر ورودی دریافت میشود این لیست شامل روابط پیوند نیافته 
 است که تبدیل به لیستی شامل تمام حالت های پیوند دوتایی روابط داده شده خواهد شد
 و در فراخوانی بعدی این لیست به ازای تمام روابط موجود در آن در لیست اولیه پیوند
 داده خواهد شد تا تمام حالت های پیوند سه تایی را داشته باشیم لازم به ذکر است 
 اطلاعات مورد نیاز برای پیوند های جدید که شامل هزینه های مرحله قبل است در این
  لیست ذخیره شده است و به سهولت توسط تابع مورد نظر قابل دست رسی خواهد بود
  این تابع بازگشتی توسط متغییری از جنس عددی که به تعداد رابطه های 
 کنترل خواهد شد به این صورت که به تعداد این روابط ما درجه پیوند خواهیم داشت
  مثلا به ازای ۴ رابطه ورودی یک رابطه را میتوان ۴ بار پیوند داد و با هر بار فراخوانی 
  یک واحد از این مقدار کم خواهد شد و در نهایت با رسیدن به مقدار ۱ این حلقه به اتمام خواهد رسید


                            **ساختمان پروژه**

پروژه شامل ۴ کلاس به شرح زیر است

 Variable => متغییر های روابط توسط این کلاس تعیین میشود شامل تابع ویژه ای نیست و دو متغیر
             اصلی مقدار متغییر و نام آن را شامل میشود
 Relation => این کلاس بیان کننده ساختار رابطه و توابع انجام عملیات ها روی آن هاست 
 که شامل پیوند دو رابطه که در تابع پیوند دو لیست از روابط به کمک می آید  

 Common => در این کلاس توابع پر کاربرد همانند یافتن کمترین هزینه بین روابط موجود پیاده سازی شده و دریافت متغیر های ورودی از کاربر است   

 Program => فراخوانی های مورد نیاز در این قسمت انجام میشوند

/////////////////////               END                   ////////////////////////











﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace DBprojectFinal
{
    #region Variable Class
    public class Variable
    {
        public string name;
        public int value;
        public Variable(string n, int v) { 
            this.name = n;
            this.value = v;
        }
    }
    #endregion
    #region Result Relation
    public class ResultInterFace
    {
        public Relation minRelation { get; set; }
        public int minTRelation { get; set; }
    }
    #endregion
    #region Relation Class
    public class Relation
    {

        /// <summary>
        /// static property that get of user
        /// </summary>
        public static List<Relation> entryRelation { get; set; }

        /// <summary>
        /// Cost of Relation
        /// </summary>
        public int T_Relation { get; set; }

        /// <summary>
        /// Variables of an relation
        /// </summary>
        public List<Variable> values  { get; set; }

        /// <summary>
        /// number of total relation
        /// </summary>
        public int num_relation;

        #region Ctor

        public Relation(List<Variable> list)
        {
            this.values = list; 
            this.T_Relation = 0;
        }

        public Relation()
        {}
        public Relation(int numberOfRelation)
        {
            ///this.T_Relation = t;
            values = new List<Variable>();
            num_relation = numberOfRelation;
        }
        #endregion

        #region Joins Methods and recursive

        /// <summary>
        /// this method join two Relation by extentions method and compute cost of twice relation
        /// </summary>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <returns></returns>
        public Relation unitJoin(Relation r1, Relation r2)
        {
            int multiOfMaxValue = 1;
            List<Variable> newValue = new List<Variable>();
            Relation newRel = new Relation(null);
            /*
            foreach (Variable value1 in r1.values)
            {
                newValue.Add(value1);
                foreach (Variable value2 in r2.values)
                {
                    newValue.Add(value2);
                    if (value1.name == value2.name)
                    {
                        if (value1.value > value2.value) 
                            multiOfMaxValue = value1.value * multiOfMaxValue;
                        else
                            multiOfMaxValue = value2.value * multiOfMaxValue;
                    }
                }
            }*/
            newRel.values = newValue;
            newRel.T_Relation = ((r1.T_Relation * r2.T_Relation) / multiOfMaxValue);
            return newRel;
        }

        /// <summary>
        /// this method get preList and compute unitJoin and Multiplication in static list that get from user
        /// </summary>
        /// <param name="preList"></param>
        /// <returns></returns>
        public List<Relation> totalJoin(List<Relation> preList)
        {
            List<Relation> newList = new List<Relation>();
            foreach (Relation r1 in preList)
                foreach (Relation r2 in entryRelation)
                    newList.Add(unitJoin(r1, r2));
            return newList;
        }

        /// <summary>
        /// Recursive method for compute total relation
        /// </summary>
        /// <param name="rList"></param>
        /// <returns></returns>
        public List<Relation> getOrder(List<Relation> rList)
        {
            if (num_relation == 1)
                return totalJoin(entryRelation);
            else
                return getOrder(totalJoin(rList));
        }
        #endregion


    }
    #endregion
    #region Common Class
    public static class Common
    {
        /// <summary>
        /// This Method Get Variable and Compute Cost 
        /// </summary>
        public static void GetValueAndCompute()
        {
            Relation.entryRelation = new List<Relation>();
            Console.WriteLine("Enter number of Relatiion");
            string numberOfRelation = Console.ReadLine();
            Relation R = new Relation(int.Parse(numberOfRelation));
            for (int i = 1; i <= Int32.Parse(numberOfRelation); i++)
            {
                Variable v1, v2;
                Console.WriteLine("Enter info of Relatiion: " + i + "\n");
                Console.WriteLine("Number of Topel \n");
                string topel = Console.ReadLine();


                Console.WriteLine("Enter name of first variable \n");
                string v1name = Console.ReadLine();

                Console.WriteLine("Enter value of first variable \n");
                int v1value = Int32.Parse(Console.ReadLine());

                Console.WriteLine("Enter name of second variable \n");
                string v2name = Console.ReadLine();

                Console.WriteLine("Enter value of second variable \n");
                int v2value = Int32.Parse(Console.ReadLine());

                v1 = new Variable(v1name, v2value);
                v2 = new Variable(v2name, v2value);
                List<Variable> vList = new List<Variable>();
                vList.Add(v1);
                vList.Add(v2);
                Relation r = new Relation(vList);
 //               r.values.Add(v1);
 //               r.values.Add(v2);

                Relation.entryRelation.Add(r);
            }
            R.GetMinimumOfTRelation().PrintObject();
        }

        /// <summary>
        /// This Method Get Relation and find minRelation
        /// </summary>
        /// <param name="relation"></param>
        /// <returns></returns>
        public static ResultInterFace GetMinimumOfTRelation(this Relation relation)
        {
            List<Relation> orderRelations = relation.getOrder(Relation.entryRelation);
            ResultInterFace minRelation = FindMin(orderRelations);
            return minRelation;
            #region Nested Method
            ResultInterFace FindMin(List<Relation> orderRelations)
            {
                int min = int.MaxValue;
                foreach (Relation orderRelation in orderRelations)
                    if (orderRelation.T_Relation < min)
                        min = orderRelation.T_Relation;
                return new ResultInterFace
                {
                    minRelation = orderRelations.Where(C => C.T_Relation.Equals(min)).FirstOrDefault(),
                    minTRelation = min
                };
            }
            #endregion
        }

        /// <summary>
        /// this method print object
        /// </summary>
        /// <param name="obj"></param>
        public static void PrintObject(this ResultInterFace obj) 
        {
            Console.WriteLine
                (
                    $"Min Relation Cost: => {obj.minTRelation} \n" +
                    $"Relation Variables: => " +
                    $"number Relation: {obj.minRelation.num_relation}"
                );
        }
    }
    #endregion
    #region Program Class
    class Program
    {
        static void Main(string[] args)
        {
            Common.GetValueAndCompute();
        }
    }
    #endregion
}
