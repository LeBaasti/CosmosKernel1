using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1
{
    internal class Memory
    {
        Cosmos.Core.ManagedMemoryBlock newBlock= new Cosmos.Core.ManagedMemoryBlock(16);
        public void WriteAt(uint index, byte value){
            newBlock.Write8(index, value);
        }
        //Memory Block auslesen
        public ushort ReadAt(uint index){
            ushort retVal= 0;
            retVal = newBlock.Read16(index);
            return retVal;
        }
    }
}
