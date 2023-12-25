using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wftest;

public class HuffmanCoding
{
    private class HuffmanNode
    {
        public char Data;
        public int Frequency;
        public HuffmanNode Left;
        public HuffmanNode Right;
    }

    private class HuffmanComparer : IComparer<HuffmanNode>
    {
        public int Compare(HuffmanNode x, HuffmanNode y)
        {
            return x.Frequency - y.Frequency;
        }
    }

    private HuffmanNode root;

    private static HuffmanNode HuffmanTree(PriorityQueue<HuffmanNode> pq)
    {
        while (pq.Count > 1)
        {
            var left = pq.Dequeue();
            var right = pq.Dequeue();

            var internalNode = new HuffmanNode
            {
                Data = '$',
                Frequency = left.Frequency + right.Frequency,
                Left = left,
                Right = right
            };

            pq.Enqueue(internalNode);
        }

        return pq.Peek();
    }

    private static void HuffmanCode(HuffmanNode root, string code, Dictionary<char, string> huffmanCode)
    {
        if (root != null)
        {
            if (root.Data != '$')
            {
                huffmanCode[root.Data] = code;
            }

            HuffmanCode(root.Left, code + "0", huffmanCode);
            HuffmanCode(root.Right, code + "1", huffmanCode);
        }
    }

    private static string Encode(string text, Dictionary<char, string> huffmanCodes)
    {
        var encode = new StringBuilder();
        foreach (var c in text)
        {
            encode.Append(huffmanCodes[c]);
        }
        return encode.ToString();
    }

    private static string Decode(Dictionary<char, string> huffmanCodes, string encode, int readAddText)
    {
        var decode = new StringBuilder();
        var currentCode = "";

        for (var i = 0; i < encode.Length - readAddText; i++)
        {
            var bit = encode[i];
            currentCode += bit;

            foreach (var entry in huffmanCodes.Where(entry => entry.Value == currentCode))
            {
                decode.Append(entry.Key);
                currentCode = "";
                break;
            }
            // Use 'bit' within the loop
        }

        return decode.ToString();
    }

    public static void CompressAndSaveToFile(string inputFileName, string outputFileName)
    {
        var inputFile = File.ReadAllText(inputFileName);
        var frequencyMap = inputFile.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());

        var minHeap = new PriorityQueue<HuffmanNode>(new HuffmanComparer());
        foreach (var entry in frequencyMap)
        {
            var newNode = new HuffmanNode
            {
                Data = entry.Key,
                Frequency = entry.Value,
                Left = null,
                Right = null
            };
            minHeap.Enqueue(newNode);
        }

        var huffmanTree = HuffmanTree(minHeap);

        var huffmanCodes = new Dictionary<char, string>();
        HuffmanCode(huffmanTree, "", huffmanCodes);

        var encodedText = Encode(inputFile, huffmanCodes);

        using (var encodedFile = new BinaryWriter(File.Open(outputFileName, FileMode.Create)))
        {
            int huffAdd = 0;
            int huffLen = 0;
            foreach (var entry in huffmanCodes)
            {
                var character = entry.Key;
                encodedFile.Write(character);

                var code = entry.Value;
                huffAdd = 8 - code.Length % 8;
                huffLen = code.Length / 8 + 1;

                encodedFile.Write(huffAdd);
                encodedFile.Write(huffLen);

                code += new string('0', huffAdd);
                for (var i = 0; i < huffLen; i++)
                {
                    var bits = new BitArray(code.Substring(i * 8, 8).Select(b => b == '1').ToArray());
                    var byteArray = new byte[1];
                    bits.CopyTo(byteArray, 0);
                    encodedFile.Write(byteArray);
                }
            }

            var separator = '#';
            encodedFile.Write(separator);

            int addText = 0;
            var encodedTextLength = encodedText.Length;
            if (encodedTextLength % 8 != 0)
            {
                addText = 8 - encodedTextLength % 8;
                encodedText += new string('0', addText);
            }
            var len = encodedText.Length / 8;
            encodedFile.Write(addText);
            encodedFile.Write(len);

            for (var i = 0; i < len; i++)
            {
                var bits = new BitArray(encodedText.Substring(i * 8, 8).Select(b => b == '1').ToArray());
                var byteArray = new byte[1];
                bits.CopyTo(byteArray, 0);
                encodedFile.Write(byteArray);
            }
        }
    }

    public static void DecompressAndSaveToFile(string inputFileName, string outputFileName)
    {
        using (var readEncodedFile = new BinaryReader(File.Open(inputFileName, FileMode.Open)))
        {
            var readHuffmanCodes = new Dictionary<char, string>();
            char character;
            var readBits = new BitArray(8);
            int readHuffAdd = 0;
            int readHuffLen = 0;

            while ((character = readEncodedFile.ReadChar()) != '#')
            {
                string bitsString = "";
                readHuffAdd = readEncodedFile.ReadInt32();
                readHuffLen = readEncodedFile.ReadInt32();

                for (var i = 0; i < readHuffLen; i++)
                {
                    readBits = new BitArray(new[] { readEncodedFile.ReadByte() });
                    bitsString += string.Join("", readBits.Cast<bool>().Select(bit => bit ? "1" : "0"));
                }

                bitsString = bitsString.Substring(0, bitsString.Length - readHuffAdd);
                readHuffmanCodes[character] = bitsString;
            }

            var readAddText = readEncodedFile.ReadInt32();
            var readLen = readEncodedFile.ReadInt32();

            var readEncodedText = "";
            for (var i = 0; i < readLen; i++)
            {
                readBits = new BitArray(new[] { readEncodedFile.ReadByte() });
                readEncodedText += string.Join("", readBits.Cast<bool>().Select(bit => bit ? "1" : "0"));
            }

            var decodedText = Decode(readHuffmanCodes, readEncodedText, readAddText);

            File.WriteAllText(outputFileName, decodedText);
        }
    }
}

public class PriorityQueue<T>
{
    private readonly IComparer<T> comparer;
    private readonly List<T> heap;

    public PriorityQueue(IComparer<T> comparer)
    {
        this.comparer = comparer;
        this.heap = new List<T>();
    }

    public int Count => this.heap.Count;

    public void Enqueue(T item)
    {
        this.heap.Add(item);
        int i = this.Count - 1;

        while (i > 0)
        {
            int parent = (i - 1) / 2;

            if (this.comparer.Compare(this.heap[parent], this.heap[i]) < 0)
            {
                break;
            }

            T temp = this.heap[i];
            this.heap[i] = this.heap[parent];
            this.heap[parent] = temp;

            i = parent;
        }
    }

    public T Dequeue()
    {
        int count = this.Count - 1;

        if (count < 0)
        {
            throw new InvalidOperationException("Queue is empty");
        }

        T frontItem = this.heap[0];
        this.heap[0] = this.heap[count];
        this.heap.RemoveAt(count);

        count--;

        int parent = 0;
        while (true)
        {
            int leftChild = parent * 2 + 1;
            if (leftChild > count)
            {
                break;
            }

            int rightChild = leftChild + 1;
            if (rightChild <= count && this.comparer.Compare(this.heap[rightChild], this.heap[leftChild]) < 0)
            {
                leftChild = rightChild;
            }

            if (this.comparer.Compare(this.heap[leftChild], this.heap[parent]) < 0)
            {
                T temp = this.heap[parent];
                this.heap[parent] = this.heap[leftChild];
                this.heap[leftChild] = temp;
            }
            else
            {
                break;
            }

            parent = leftChild;
        }

        return frontItem;
    }
    public T Peek()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("Queue is empty");
        }

        return this.heap[0];
    }
}
