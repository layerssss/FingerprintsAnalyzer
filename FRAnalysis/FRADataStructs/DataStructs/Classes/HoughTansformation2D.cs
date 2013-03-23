using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs.Classes
{
    public class HoughTansformation2D
    {
        public HoughTansformation2D(int width, int height, int deltaMoving, int votesMoving, double angle, double deltaAngle, double votesAngle)
        {
            this.width = width;
            this.height = height;
            this.deltaW = deltaMoving;
            this.votesMoving = votesMoving;
            this.angle = angle;
            this.deltaAngle = deltaAngle;
            this.votesAngle = votesAngle;

            this.wSteps = width / deltaMoving;
            this.hSteps = height / deltaMoving;
            this.aSteps = (int)(angle / deltaAngle);

            this.bin = new int[this.hSteps * 2 + 1][][];
            for (var i = 0; i < this.bin.Length; i++)
            {
                this.bin[i] = new int[this.wSteps * 2 + 1][];
                for (var j = 0; j < this.bin[i].Length; j++)
                {
                    this.bin[i][j] = new int[this.aSteps * 2 + 1];
                    for (var a = 0; a < this.bin[i][j].Length; a++)
                    {
                        this.bin[i][j][a] = 0;
                    }
                }
            }
        }
        void vote(int i, int j, double angle, int weight)
        {
            var iStep = i / deltaW;
            var jStep = j / deltaW;
            var aStep = (int)(angle / deltaAngle);
            if (Math.Abs(iStep) > this.hSteps || Math.Abs(jStep) > this.wSteps || Math.Abs(aStep) > this.aSteps)
            {
                return;
            }
            this.bin[this.hSteps + iStep][this.wSteps + jStep][this.aSteps + aStep] += weight;
        }
        public void Vote(PointLocation pl1,double angle1,PointLocation pl2,double angle2,int weight)
        {
            var angle = (angle1 - angle2 + Math2.PI(3)) % Math2.PI(2) - Math.PI;
            var cos = Math.Cos(-angle);
            var sin = Math.Sin(-angle);
            var i = (int)(pl1.I * cos - pl1.J * sin - pl2.I);
            var j = (int)(pl1.J * cos + pl1.I * sin - pl2.J);
            for (var deltaI = 0; deltaI < this.votesMoving; deltaI += this.deltaW)
            {
                for (var deltaJ = 0; deltaJ < this.votesMoving; deltaJ += this.deltaW)
                {
                    for (var deltaA = 0.0; deltaA < this.votesAngle; deltaA += this.deltaAngle)
                    {
                        this.vote(i + deltaI, j + deltaJ, angle + deltaA, weight);
                        this.vote(i + deltaI, j - deltaJ, angle + deltaA, weight);
                        this.vote(i + deltaI, j + deltaJ, angle - deltaA, weight);
                        this.vote(i + deltaI, j - deltaJ, angle - deltaA, weight);
                        this.vote(i - deltaI, j + deltaJ, angle + deltaA, weight);
                        this.vote(i - deltaI, j - deltaJ, angle + deltaA, weight);
                        this.vote(i - deltaI, j + deltaJ, angle - deltaA, weight);
                        this.vote(i - deltaI, j - deltaJ, angle - deltaA, weight);
                    }
                }
            }
        }
        public int GetBest(out int deltaI, out int deltaJ, out double deltaAngle)
        {
            int max = 0;
            int dI=0;
            int dJ=0;
            int dA=0;
            for (var i = 0; i < this.bin.Length; i++)
            {
                for (var j = 0; j < this.bin[i].Length; j++)
                {
                    for (var a = 0; a < this.bin[i][j].Length; a++)
                    {
                        if (this.bin[i][j][a] > max)
                        {
                            dI = i;
                            dJ = j;
                            dA = a;
                            max = this.bin[i][j][a];
                        }
                    }
                }
            }
            deltaI = (dI - this.hSteps) * this.deltaW;
            deltaJ = (dJ - this.wSteps) * this.deltaW;
            deltaAngle = (dA - this.aSteps) * this.deltaAngle;
            return max;
        }
        int width, height, deltaW, votesMoving;
        int wSteps, hSteps, aSteps;
        double angle, deltaAngle, votesAngle;
        int[][][] bin;
    }
}
