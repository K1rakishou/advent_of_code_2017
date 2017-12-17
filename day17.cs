using System;
using System.Collections.Generic;

namespace andventOfCode {
    public class DaySeventeen : Puzzle {
        public void run() {
            Console.WriteLine("Day Seventeen");
            PuzzleOne();
            PuzzleTwo();
        }

        public void PuzzleOne() {
            var ringBuffer = new List<int>();
            var bufferPos = 0;
            var step = 354;

            for (var i = 0; i < 2018; ++i) {
                if (ringBuffer.Count == 0) {
                    bufferPos = 0;
                }
                else {
                    bufferPos = (bufferPos + step) % ringBuffer.Count;
                    ++bufferPos;
                }

                ringBuffer.Insert(bufferPos, i);
            }


            var result = ringBuffer[bufferPos + 1];
            Console.WriteLine(String.Format("Puzzle One Answer: {0}", result));
        }

        public void PuzzleTwo() {
            var step = 354;
            var currentStep = 0;
            var size = 1;
            var value = 0;

            for (var i = 1; i <= 50000000; i++) {
                currentStep = (currentStep + step) % size++;
                if (currentStep == 0) {
                    value = i;
                }

                currentStep++;
            }

            Console.WriteLine(String.Format("Puzzle Two Answer: {0}", value));
        }
    }
}