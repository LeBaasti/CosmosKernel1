using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmos.Core;
using Cosmos.Core.IOGroup;
using Cosmos.System;

namespace KernelProject_One
{
    public static class MemoryMaster
    {

        public static readonly int CountOfMemBlocks = 4;
        public static ManagedMemoryBlock[] MemoryBlocks = new ManagedMemoryBlock[CountOfMemBlocks];

        public static void InitializeBlock(uint BlockSize, int BlockIndex)
        {
            if (MemoryBlocks[BlockIndex] == null)
            {
                MemoryBlocks[BlockIndex] = new ManagedMemoryBlock(BlockSize);
            }
        }

        private static void WriteValue(byte Offset, UInt16 Value, int BlockIndex)
        {
            MemoryBlocks[BlockIndex].Write16(Offset, Value);
        }

        private static void WriteValue(byte Offset, UInt32 Value, int BlockIndex)
        {
            MemoryBlocks[BlockIndex].Write32(Offset, Value);
        }

        private static void WriteValue(byte Offset, byte Value, int BlockIndex)
        {
            MemoryBlocks[BlockIndex].Write8(Offset, Value);
        }
    }

    public struct MemoryBlock
    {
        public static readonly uint CountOfMemBlocks = 64;
        private static ManagedMemoryBlock MemoryBlocks = new ManagedMemoryBlock(CountOfMemBlocks);

        public dynamic Value { get; private set; }

        public static int[] ProgramIndex = new int[CountOfMemBlocks];

        public static void InitializeBlock(uint BlockSize, int BlockIndex)
        {
            MemoryBlocks = new ManagedMemoryBlock(BlockSize);
        }

        private static void WriteValue(byte Offset, UInt16 Value, uint BlockIndex)
        {
            MemoryBlocks.Write16(Offset, Value);
        }

        private static void WriteValue(byte Offset, UInt32 Value, uint BlockIndex)
        {
            MemoryBlocks.Write32(Offset, Value);
            
        }

        private static void WriteValue(byte Offset, byte Value, uint BlockIndex)
        {
            MemoryBlocks.Write8(Offset, Value);
        }
    }
}
