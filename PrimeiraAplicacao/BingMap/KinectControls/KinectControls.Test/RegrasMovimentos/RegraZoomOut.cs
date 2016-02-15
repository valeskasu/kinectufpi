using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using KinectControls.Test.RegrasMovimentos.FuncoesAuxiliares;

namespace KinectControls.Test.RegrasMovimentos
{
    public class RegraZoomOut
    {

        public static bool ExecutaRegraZoomOut(Skeleton body)
        {

            Joint centroOmbros = body.Joints[JointType.ShoulderCenter];

            Joint handLeft = body.Joints[JointType.HandLeft];
            //Joint elbowLeft = body.Joints[JointType.ElbowLeft];

            Joint handRight = body.Joints[JointType.HandRight];
            //Joint elbowRight = body.Joints[JointType.ElbowRight];

            Joint shoulderRight = body.Joints[JointType.ShoulderRight];
            Joint shoulderLeft = body.Joints[JointType.ShoulderLeft];

            double margemErro = 0.15;

            bool maoDireitaAlturaCorreta =
                Util.CompararComMargemErro(margemErro,
                handRight.Position.Y, centroOmbros.Position.Y);

            bool maoDireitaDistanciaCorreta =
                Util.CompararComMargemErro(margemErro,
                handRight.Position.Z, centroOmbros.Position.Z);

            bool maoDireitaAposCotovelo =
                handRight.Position.X > shoulderRight.Position.X;

            bool cotoveloDireitoAlturaCorreta =
                Util.CompararComMargemErro(margemErro,
                shoulderRight.Position.Y, centroOmbros.Position.Y);

            bool cotoveloEsquerdoAlturaCorreta =
                Util.CompararComMargemErro(margemErro,
                shoulderLeft.Position.Y, centroOmbros.Position.Y);

            bool maoEsquerdaAlturaCorreta =
                Util.CompararComMargemErro(margemErro,
                handLeft.Position.Y, centroOmbros.Position.Y);

            bool maoEsquerdaDistanciaCorreta =
                Util.CompararComMargemErro(margemErro,
                handLeft.Position.Z, centroOmbros.Position.Z);

            bool maoEsquerdaAposCotovelo =
                handLeft.Position.X < shoulderLeft.Position.X;


            return maoDireitaAlturaCorreta &&
               maoDireitaDistanciaCorreta &&
               maoDireitaAposCotovelo &&
               cotoveloDireitoAlturaCorreta &&
               maoEsquerdaAlturaCorreta &&
               maoEsquerdaDistanciaCorreta &&
               maoEsquerdaAposCotovelo &&
               cotoveloEsquerdoAlturaCorreta;


        }


    }
}
