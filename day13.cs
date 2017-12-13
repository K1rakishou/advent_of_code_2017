using System;
using System.Linq;

namespace andventOfCode {
    public class DayThirteen : Puzzle {
        public void run() {
            Console.WriteLine("Day Thirteen");
            PuzzleOne();
            PuzzleTwo();
        }

        public void PuzzleOne() {
            var input =
                "0: 3\n1: 2\n2: 4\n4: 6\n6: 4\n8: 6\n10: 5\n12: 8\n14: 8\n16: 6\n18: 8\n20: 6\n22: 10\n24: 8\n26: 12\n28: 12\n30: 8\n32: 12\n34: 8\n36: 14\n38: 12\n40: 18\n42: 12\n44: 12\n46: 9\n48: 14\n50: 18\n52: 10\n54: 14\n56: 12\n58: 12\n60: 14\n64: 14\n68: 12\n70: 17\n72: 14\n74: 12\n76: 14\n78: 14\n82: 14\n84: 14\n94: 14\n96: 14";
            var layersInfo = input.Split('\n')
                .Select(it => it.Split(new[] {": "}, StringSplitOptions.RemoveEmptyEntries))
                .ToList();

            var severity = 0;

            foreach (var info in layersInfo) {
                var layer = Convert.ToInt32(info[0]);
                var depth = Convert.ToInt32(info[1]);

                if (layer % (2 * depth - 2) == 0) {
                    severity += layer * depth;
                }
            }

            Console.WriteLine(String.Format("Puzzle One Answer: {0}", severity));
        }

        public void PuzzleTwo() {
            var input =
                "0: 3\n1: 2\n2: 4\n4: 6\n6: 4\n8: 6\n10: 5\n12: 8\n14: 8\n16: 6\n18: 8\n20: 6\n22: 10\n24: 8\n26: 12\n28: 12\n30: 8\n32: 12\n34: 8\n36: 14\n38: 12\n40: 18\n42: 12\n44: 12\n46: 9\n48: 14\n50: 18\n52: 10\n54: 14\n56: 12\n58: 12\n60: 14\n64: 14\n68: 12\n70: 17\n72: 14\n74: 12\n76: 14\n78: 14\n82: 14\n84: 14\n94: 14\n96: 14";
            var layersInfo = input.Split('\n')
                .Select(it => it.Split(new[] {": "}, StringSplitOptions.RemoveEmptyEntries))
                .ToList();

            var delay = 0;

            while (true) {
                var caught = false;
                
                foreach (var info in layersInfo) {
                    var layer = Convert.ToInt32(info[0]);
                    var depth = Convert.ToInt32(info[1]);

                    if ((layer + delay) % (2 * depth - 2) == 0) {
                        ++delay;
                        caught = true;
                        break;
                    }
                }

                if (!caught) {
                    break;
                }
            }
            
            Console.WriteLine(String.Format("Puzzle Two Answer: {0}", delay));
        }
    }
}