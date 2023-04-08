using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Rx{
    static class Libs{
        //make sure that the random seed is completely different each time
        private static long initial = ~((~(long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds<<1)^((long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds)); //bit shuffling on current unix time 
        private static string str = initial.ToString(); //prepare for hashing
        static MD5 md5Hasher = MD5.Create();
        private static long randomSeed = BitConverter.ToInt64(md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(str)), 0); //hash str then converts it to long so that even if you would start two at 1 sec of difference it would be two completely different values

        public static long RNG(long min, long max){
            if (min > max) throw new ArgumentException("the minimum cannot be bigger than the maximum");
            long r1 = (long)UnityEngine.Random.Range((float)min, (float)max); // get a random number
            var rand = new System.Random();
            long r2 = (long)rand.Next(); // get another random number
            long r3 = BitConverter.ToInt64(md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(((long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString())), 0); //hashing go brrrrrr
            long r4 = ~((~(r1^r2))<<1)^((~(r3^randomSeed))>>3); // bit shuffling go brrrrr
            randomSeed = ~((r4<<1)^~(randomSeed>>1)); //more bit shuffling
            
            return min+Math.Abs(randomSeed%max); // make sure the value is between the min and the max
        }

        public static unsafe double RNG(double min, double max){
            if (min > max) throw new ArgumentException("the minimum cannot be bigger than the maximum");
            double r1 = UnityEngine.Random.Range((float)min, (float)max);// get a random number
            long* ptr1 = (long*)&r1; // ptr for bit shuffling (you can't do bit manipulation on floats)
            var rand = new System.Random();
            double r2 = rand.NextDouble() * RNG((long)min, (long)max); // get more random number and multiply it with Random function
            long* ptr2 = (long*)&r2; // ptr go brrrr
            double r3 = BitConverter.ToDouble(md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(((long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString())), 0); //hashing go brrrr
            long* ptr3 = (long*)&r3; // ptr go brrrr
            long r4 = ~((~(*ptr1^*ptr2))<<1)^((~(*ptr3^randomSeed))>>3); // bit shuffling go brrrrr
            randomSeed = ~((r4<<1)^~(randomSeed>>1)); // bit shuffling go brrrrr
            long rand1 = randomSeed; // can't take a pointer to a class member
            double* ptr4 = (double*)&rand1; //return to a float
            double r5 =  *ptr4;
            bool good = false;
            while (!good){
                if (r5 > max){
                    r5 -= max;
                }else if (r5 < min){
                    r5 += min;
                }
                if (r5>= min && r5<=max){
                    good = true;
                }
            } // make sure the value is between min and max
            return r5;
        }
    }
}
