using System;

namespace OrientDb.Common
{
     public abstract class Result<TOk,TErr>
    {
        internal class OkCase : Result<TOk,TErr>
        {
            public TOk Payload { get; }

            public OkCase(TOk okPayload)
            {
                Payload = okPayload;
            }
            public override bool IsOk() => true;
            public override TOk GetOk() => Payload;

            public override TErr GetErr() => throw new InvalidCastException("ok result does not contains err value");
        }

        internal class ErrCase : Result<TOk,TErr>
        {
            public TErr Payload { get; }
            public ErrCase(TErr errPayload)
            {
                Payload = errPayload;
            }

            public override bool IsOk() => false;
            public override TOk GetOk() => throw new InvalidCastException("error result does not contains ok value");

            public override TErr GetErr() => Payload;
        }
        
       
        public static  Result<TOk,TErr> Ok(TOk payload)=>new OkCase(payload);
        public static  Result<TOk,TErr> Err(TErr errPayload) => new ErrCase(errPayload);

        public abstract bool IsOk();
        public abstract TOk GetOk();
        public abstract TErr GetErr();
    }
}