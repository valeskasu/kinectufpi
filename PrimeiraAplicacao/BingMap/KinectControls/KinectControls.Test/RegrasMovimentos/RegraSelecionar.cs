using KinectControls.Test.RegrasMovimentos.FuncoesAuxiliares;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectControls.Test.RegrasMovimentos
{
    public class RegraSelecionar
    {
        public static bool ExecutaRegraSelecionar(Skeleton body)
        {

            //Joint centroOmbros = body.Joints[JointType.ShoulderCenter];

            Joint wristLeft = body.Joints[JointType.WristLeft];

            Joint wristRight = body.Joints[JointType.WristRight];
            Joint shoulderRight = body.Joints[JointType.ShoulderRight];
            Joint shoulderLeft = body.Joints[JointType.ShoulderLeft];

            Joint elbowRight = body.Joints[JointType.ElbowRight];
            Joint elbowLeft = body.Joints[JointType.ElbowLeft];

            double margemErro = 0.15;

            bool maoDireitaAlturaCorreta =
                Util.CompararComMargemErro(margemErro,
                wristRight.Position.Y, shoulderRight.Position.Y);

            //alterado
            bool maoDireitaAntesOmbro =
                wristRight.Position.X < shoulderRight.Position.X;
            //Console.WriteLine("Direita: " + maoDireitaAntesOmbro);

            bool maoEsquerdaAlturaCorreta =
                Util.CompararComMargemErro(margemErro,
                wristLeft.Position.Y, shoulderLeft.Position.Y);


            //alterado
            bool maoEsquerdaAntesOmbro =
                wristLeft.Position.X > shoulderLeft.Position.X;
            //Console.WriteLine("Esquerda: " + maoEsquerdaAntesOmbro);

            return maoDireitaAlturaCorreta &&
               maoDireitaAntesOmbro &&
               maoEsquerdaAlturaCorreta &&
               maoEsquerdaAntesOmbro;
        }

    }
}
