
/*
    BSD 3-Clause License

    Copyright (c) 2022, brimson
    All rights reserved.

    Redistribution and use in source and binary forms, with or without
    modification, are permitted provided that the following conditions are met:

    1. Redistributions of source code must retain the above copyright notice, this
    list of conditions and the following disclaimer.

    2. Redistributions in binary form must reproduce the above copyright notice,
    this list of conditions and the following disclaimer in the documentation
    and/or other materials provided with the distribution.

    3. Neither the name of the copyright holder nor the names of its
    contributors may be used to endorse or promote products derived from
    this software without specific prior written permission.

    THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
    AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
    IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
    DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
    FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
    DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
    SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
    CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
    OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
    OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using System.Numerics;

namespace Colors
{
    class Normalization
    {
        public static void Chromaticity()
        {
            Console.WriteLine("Input color (red, green, blue)");
            
            Vector3 inputColor = new Vector3(Array.ConvertAll(Console.ReadLine().Split(','), float.Parse));

            Vector3 normalizedColor = inputColor / Vector3.Dot(inputColor, Vector3.One);

            bool correctInput = false;

            do
            {
                Console.WriteLine("Choose output:\nRG Chromaticity\nNormalized RGB");

                switch (Console.ReadLine().ToLower())
                {
                    case "rg chromaticity":
                        correctInput = true;
                        Console.WriteLine($"Output: {String.Join("\n", new Vector2(normalizedColor.X, normalizedColor.Y))}");
                        break;
                    case "normalized rgb":
                        correctInput = true;
                        Console.WriteLine($"Output: {String.Join("\n", normalizedColor)}");
                        break;
                }
            } while (correctInput == false);
        }
    }

    class Alpha
    {
        public static void Blend()
        {
            // Ask user to input source RGB color
            Console.WriteLine("Input source color (red, green, blue)");
            Vector3 sourceColor = new Vector3(Array.ConvertAll(Console.ReadLine().Split(','), float.Parse));

            // Ask user to input source alpha
            Console.WriteLine("Input source alpha (0-1)");
            float alphaScalar = Math.Clamp(float.Parse(Console.ReadLine()), 0.0f, 1.0f);
            Vector3 alphaVector = new Vector3(alphaScalar);

            // Premultiply or not
            Console.WriteLine("Do you want to pre-multiply the source? (y/n)");
            sourceColor = (Console.ReadLine() == "y") ? sourceColor * alphaVector : sourceColor;

            // Ask user to input destination RGB color
            Console.WriteLine("Input destination color (red, green, blue)");
            Vector3 destinationColor = new Vector3(Array.ConvertAll(Console.ReadLine().Split(','), float.Parse));

            bool correctInput = false;

            do
            {
                Console.WriteLine("Choose output:\nStraight Alpha\nAssociated Alpha");

                switch (Console.ReadLine().ToLower())
                {
                    case "straight alpha":
                        correctInput = true;
                        Console.WriteLine($"Output: {Vector3.Lerp(destinationColor, sourceColor, alphaScalar)}");
                        break;
                    case "associated alpha":
                        correctInput = true;
                        Console.WriteLine($"Output: {sourceColor + (destinationColor * (Vector3.One - alphaVector))}");
                        break;
                }
            } while (correctInput == false);
        }
    }
}