using System;
using System.Collections.Generic;
using System.Linq;

namespace andventOfCode {
    public class DayTen : Puzzle {
        public void run() {
            Console.WriteLine("Day Ten");
            PuzzleOne();
            PuzzleTwo();
        }

        public void PuzzleOne() {
            var input = "165,1,255,31,87,52,24,113,0,91,148,254,158,2,73,153".Split(',').Select(len => Convert.ToInt32(len)).ToList();
            var list = new List<int>();
            var offset = 0;
            var skip = 0;

            for (var i = 0; i < 256; ++i) {
                list.Add(i);
            }
            
            var listLen = list.Count;
            
            foreach (var len in input) {
                var sublist = new List<int>();

                for (var i = offset; i < len + offset; ++i) {
                    sublist.Add(list[i % listLen]);
                }

                if (sublist.Count <= 1) {
                    offset += len + skip;
                    ++skip;
                    continue;
                }
                
                sublist.Reverse();

                var j = 0;
                for (var i = offset; i < len + offset; ++i) {
                    var index = i % listLen;
                    list.RemoveAt(index);
                    list.Insert(index, sublist[j]);
                    ++j;
                }

                offset += len + skip;
                ++skip;
            }

            var answer = list[0] * list[1];
            Console.WriteLine(String.Format("Puzzle One Answer: {0}", answer));
        }

        public void PuzzleTwo() {
            var input = "165,1,255,31,87,52,24,113,0,91,148,254,158,2,73,153".ToList();
            var list = new List<int>();
            var offset = 0;
            var skip = 0;
            
            input.Add('\x11');
            input.Add('\x1F');
            input.Add('\x49');
            input.Add('\x2f');
            input.Add('\x17');
            
            for (var i = 0; i < 256; ++i) {
                list.Add(i);
            }
            
            var listLen = list.Count;

            for (var round = 0; round < 64; ++round) {
                foreach (var len in input) {
                    var sublist = new List<int>();

                    for (var i = offset; i < len + offset; ++i) {
                        sublist.Add(list[i % listLen]);
                    }

                    if (sublist.Count <= 1) {
                        offset += len + skip;
                        ++skip;
                        continue;
                    }
                
                    sublist.Reverse();

                    var j = 0;
                    for (var i = offset; i < len + offset; ++i) {
                        var index = i % listLen;
                        list.RemoveAt(index);
                        list.Insert(index, sublist[j]);
                        ++j;
                    }

                    offset += len + skip;
                    ++skip;
                }
            }
            
            var denseHash = new List<char>();

            for (var i = 0; i < 16; ++i) {
                var xored = '\x0';
                
                for (var j = 0; j < 16; ++j) {
                    xored ^= (char)list[i * 16 + j];
                }
                
                denseHash.Add(xored);
            }

            var lut = "0123456789abcdef";
            var resultStr = "";
            
            foreach (var c in denseHash) {
                var first = (c >> 4) & 0x0F;
                var second = c & 0x0F;

                resultStr += lut[first];
                resultStr += lut[second];
            }

            Console.WriteLine(String.Format("Puzzle Two Answer: {0}", resultStr));
        }
    }
}