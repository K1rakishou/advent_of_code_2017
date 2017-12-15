using System;
using System.Threading.Tasks;

namespace andventOfCode {
    public class DayFifteen : Puzzle {
        public void run() {
            Console.WriteLine("Day Fifteen");
            PuzzleOne();
            PuzzleTwo();
        }

        public void PuzzleOne() {
            var generatorA = new Generator(16807, 516);
            var generatorB = new Generator(48271, 190);
            var equalSequences = 0;

            for (var i = 0; i < 40000000; ++i) {
                var valueA = generatorA.nextValue();
                var valueB = generatorB.nextValue();

                if ((valueA & 0xFFFF) == (valueB & 0xFFFF)) {
                    ++equalSequences;
                }
            }

            Console.WriteLine(String.Format("Puzzle One Answer: {0}", equalSequences));
        }

        public void PuzzleTwo() {
            var generatorA = new Generator2(16807, 516, 4);
            var generatorB = new Generator2(48271, 190, 8);
            var equalSequences = 0;

            for (var i = 0; i < 5000000; ++i) {
                var valueA = generatorA.nextValue();
                var valueB = generatorB.nextValue();

                if ((valueA & 0xFFFF) == (valueB & 0xFFFF)) {
                    ++equalSequences;
                }
            }

            Console.WriteLine(String.Format("Puzzle Two Answer: {0}", equalSequences));
        }

        class Generator {
            private long factor;
            private long value;

            public Generator(long factor, long value) {
                this.factor = factor;
                this.value = value;
            }

            public long nextValue() {
                value = (value * factor) % 2147483647;
                return value;
            }
        }

        class Generator2 {
            private long factor;
            private long value;
            private long lookupValue;

            public Generator2(long factor, long value, long lookupValue) {
                this.factor = factor;
                this.value = value;
                this.lookupValue = lookupValue;
            }

            public long nextValue() {
                do {
                    value = (value * factor) % 2147483647;
                } while (value % lookupValue != 0);

                return value;
            }
        }
    }
}