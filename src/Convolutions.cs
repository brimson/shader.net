
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

namespace Convolutions;

class GaussianBlur
{
    public static float Weight(float pixelIndex, float kernelSize)
    {
        var pi = 3.141592653589793238462643f;
        var sigma = kernelSize / 3.0f;
        var output = 1.0f / MathF.Sqrt(2.0f * pi * (sigma * sigma));
        return output * MathF.Exp(-(pixelIndex * pixelIndex) / (2.0f * (sigma * sigma)));
    }

    public static void Calculate()
    {
        Console.WriteLine("Enter kernel size:");
        int kernelTaps = Convert.ToInt32(Console.ReadLine());

        // Initialize list of weights and offsets to print
        var weightList = new List<float>();
        var offsetList = new List<float>();

        // Calculate center tap first
        weightList.Add(Weight(0, kernelTaps));
        offsetList.Add(0.0f);

        // Initialize loop parameters
        int pixelIndex = 1;
        int valueIndex = 0;
        Vector2 pixelWeight = new Vector2();
        Vector2 pixelOffset = new Vector2();

        // Remaining taps (negate offsets for left-sided taps)
        while (pixelIndex < kernelTaps)
        {
            pixelOffset.X = pixelIndex;
            pixelOffset.Y = pixelIndex + 1;
            pixelWeight.X = Weight(pixelOffset.X, kernelTaps);
            pixelWeight.Y = Weight(pixelOffset.Y, kernelTaps);

            float linearWeight = pixelWeight.X + pixelWeight.Y;
            float linearOffset = Vector2.Dot(pixelOffset, pixelWeight) / linearWeight;

            offsetList.Add(linearOffset);
            weightList.Add(linearWeight);

            pixelIndex += 2;
            valueIndex += 1;
        }

        string totalOffsets = String.Join(", ", offsetList);
        string totalWeights = String.Join(", ", weightList);
        Console.WriteLine($"Offsets: {totalOffsets}\nWeights: {totalWeights}");
    }
}
