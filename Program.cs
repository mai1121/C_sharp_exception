using System;
using System.IO;//ファイル操作用の名前空間
using System.Collections.Generic;

namespace practice_exception
{
    class Program
    {
        static void Main(string[] args)
        {
            //logファイルに書き込み

            using (var sw = new StreamWriter("date.log"))
            {
                Console.WriteLine("ユーザー名を入力してください");
                var user = Console.ReadLine();
                sw.WriteLine(user);
                sw.WriteLine(DateTime.Now.ToString());
            }


            var path = "date.log";

            //finallyで参照するために変数srを定義
            StreamReader sr = null;
            try
            {
                using (sr = new StreamReader(path))
                {
                    Console.WriteLine(sr.ReadToEnd());
                }
            }
            catch(ArgumentNullException ex)
            {
                Console.WriteLine("ファイル名が指定されていません");
                Console.WriteLine(ex.Message);
            }
            catch (FileNotFoundException ex) when (ex.Message.Contains(".log"))
            {
                Console.WriteLine("存在しない.logファイルが指定されました");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("ファイルが見つかりませんでした");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
            }

            var key = 6;
            try
            {
                var m1 = new Menu();
                Console.WriteLine(m1.Lunch(key));
            }
            catch(IndexOutOfRangeException e)
            {
                Console.WriteLine($"{e.Message}{key}は無効なメニュー番号です");
            }

            try
            {
                var m2 = new Menu();
                Console.WriteLine($"今日の夕食は{m2.Dinner(key)}です");
            }
            catch(KeyNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }


        }
        class Menu
        {
            public string Lunch(int key)
            {
                //例外のスロー命令
                var dic = new Dictionary<int, string>()
                {
                    {1,"カレー"},
                    {2,"ナポリタン"},
                    {3,"豚汁"},
                    {4,"シチュー"},
                    {5,"生姜焼き"}
                };

                if (key < 0 || key >= dic.Count)
                {
                    throw new IndexOutOfRangeException();
                }
                return dic[key];
            }
            //例外の再スロー
            public string Dinner(int key)
            {
                var dic = new Dictionary<int, string>()
                {
                    {1,"カレー"},
                    {2,"ナポリタン"},
                    {3,"豚汁"},
                    {4,"シチュー"},
                    {5,"生姜焼き"}
                };
                try
                {
                    return dic[key];
                }
                catch(KeyNotFoundException)
                {
                    throw;
                }
            }

        }
    }
}
