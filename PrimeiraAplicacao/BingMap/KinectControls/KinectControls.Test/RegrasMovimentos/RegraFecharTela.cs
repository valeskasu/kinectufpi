using KinectControls.Test.RegrasMovimentos.FuncoesAuxiliares;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectControls.Test.RegrasMovimentos
{
    public class RegraFecharTela
    {

        public static bool ExecutaRegraFecharTela(Skeleton body)
        {

            //Joint centroOmbros = body.Joints[JointType.ShoulderCenter];

            Joint handLeft = body.Joints[JointType.HandLeft];
            Joint handRight = body.Joints[JointType.HandRight];

            Joint head = body.Joints[JointType.Head];
            
            
            bool maoDireitaAlturaCorreta =
                 (handRight.Position.Y> head.Position.Y);
            
            bool maoEsquerdaAlturaCorreta =
                 (handLeft.Position.Y> head.Position.Y);


            return maoDireitaAlturaCorreta &&
               maoEsquerdaAlturaCorreta;
        }



    }
}
