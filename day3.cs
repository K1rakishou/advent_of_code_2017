using System;

namespace andventOfCode {
    public class DayThree : Puzzle {
        public void run() {
            Console.WriteLine("Day Three");
            PuzzleOne();
            PuzzleTwo();
        }

        private void PuzzleOne() {
            var squareIndex = 1;
            var x = 0;
            var y = 0;
            var direction = 0; //0 - right, 1 - up, 2 - left, 3 - down
            var step = 1;
            var targetSquareIndex = 265149;

            while (true) {
                switch (direction) {
                    case 0:
                        x += step;
                        y = 0;
                        break;
                    case 1:
                        y += step;
                        x = 0;
                        break;
                    case 2:
                        x += -step;
                        y = 0;
                        break;
                    default:
                        y += -step;
                        x = 0;
                        break;
                }

                squareIndex += step;
                if (squareIndex >= targetSquareIndex) {
                    var delta = squareIndex - targetSquareIndex;

                    switch (direction) {
                        case 0:
                            x -= delta;
                            y = 0;
                            break;
                        case 1:
                            y -= delta;
                            x = 0;
                            break;
                        case 2:
                            x += delta;
                            y = 0;
                            break;
                        default:
                            y += delta;
                            x = 0;
                            break;
                    }

                    break;
                }

                ++direction;
                direction %= 4;

                if (direction % 2 == 0) {
                    ++step;
                }
            }

            var dist = (Math.Abs(x) - 0) + (Math.Abs(y) - 0);
            Console.WriteLine(String.Format("Puzzle One Answer: {0}", dist));
        }

        private void PuzzleTwo() {
            var x = 0;
            var y = 0;
            var direction = 0; //0 - right, 1 - up, 2 - left, 3 - down
            var targetSquareIndex = 265149;
            var array = new int[100, 100];
            var value = 0;
            var arraySide = 100;
            var halfSize = arraySide / 2;
            var step = 0;
            var walkDist = 1;
            var check = 0;
            Array.Clear(array, 0, array.Length);
            array[halfSize, halfSize] = 1;
            
            while (true) {
                switch (direction) {
                    case 0:
                        ++x;
                        break;
                    case 1:
                        ++y;
                        break;
                    case 2:
                        --x;
                        break;
                    default:
                        --y;
                        break;
                }
                
                value = checkAround(array, x + halfSize, y + halfSize);
                array[x + halfSize, y + halfSize] = value;
                
                if (value > targetSquareIndex) {
                    break;
                }

                ++step;
                if (step % walkDist == 0) {
                    ++check;
                    
                    ++direction;
                    direction %= 4;
                }

                if (check % 2 == 0 && check > 0) {
                    check = 0;
                    step = 0;
                    
                    ++walkDist;
                }
            }

            Console.WriteLine(String.Format("Puzzle Two Answer: {0}", value));
        }

        private int checkAround(int[,] array, int x, int y) {
            var sum = 0;

            sum += array[x - 1, y - 1];
            sum += array[x - 1, y];
            sum += array[x - 1, y + 1];
            sum += array[x, y - 1];
            sum += array[x, y + 1];
            sum += array[x + 1, y - 1];
            sum += array[x + 1, y];
            sum += array[x + 1, y + 1];

            return sum;
        }
    }
}























