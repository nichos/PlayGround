using System;
using System.Collections.Generic;
using System.Linq;
namespace Exam1
{
    class Program
    {
        static void Main(string[] args)
        {
			var input = new[] { 7, 10, 11, 5, 2, 5, 5, 7, 11, 8, 9 };
			var k = 4;
			
			FindMostFrequenceNo(input, k);
            Console.ReadLine();
        }
		
		private static void FindMostFrequenceNo(int[] inputs, int k){
			var frequecyDic = new Dictionary<int, int>();
			for(var i=0;i<inputs.Length;i++){
				if(frequecyDic.ContainsKey(inputs[i])){
					frequecyDic[inputs[i]]++;
				} 
				else {
					frequecyDic.Add(inputs[i], 1);
				}
			}
			
			var topKeyValues = frequecyDic.OrderByDescending(o=>o.Value).ThenByDescending(o=>o.Key).Take(k);
			foreach(var keyValue in topKeyValues){
				Console.Write(keyValue.Key + " ");
			}
		}
    }
}
