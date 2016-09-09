using System;

    internal class PrintConsole
    {
        static int errorCount = 0;
        static int infoCount = 0;

        /// <summary>
        /// Hata bildirimi yapmak için kullanılır
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        public static void LOG(String source, String message)
        {
            errorCount++;
            Console.WriteLine("Run-time Error No : " + errorCount);
            Console.WriteLine("<ERROR>\n\t<SOURCE>\n\t{0}\n\t</SOURCE>\n\t<MESSAGE>\n\t{1}\n\t</MESSAGE>\n</ERROR>\n", source, message);
        }
        
        /// <summary>
        /// Hata olmayacak düzeydeki bildirimi yapmak için kullanılır
        /// </summary>
        /// <param name="message"></param>
        public static void _INFO(String message)
        {
            infoCount++;
            Console.WriteLine("Run-Time Info No : {0}", errorCount);
            Console.WriteLine("<INFO>\n\t<MESSAGE>\n\t{0}\n\t</MESSAGE>\n</INFO>\n", message);
        }

    }

