using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using KinectControls.Test.RegrasMovimentos.FuncoesAuxiliares;
namespace KinectControls.Test.RegrasMovimentos
{
    public class RegraSelecionar
    {
        public static bool ExecutaRegraSelecionar(Skeleton body)
        {
            Joint wristLeft = body.Joints[JointType.WristLeft];
            Joint wristRight = body.Joints[JointType.WristRight];

            double margemErro = 0.15;

            return Util.CompararComMargemErro(margemErro, wristLeft.Position.Z, wristRight.Position.Z);
        }
    }
}