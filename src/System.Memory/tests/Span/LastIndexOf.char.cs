// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Xunit;

namespace System.SpanTests
{
    public static partial class SpanTests
    {
        [Fact]
        public static void ZeroLengthLastIndexOf_Char()
        {
            Span<char> sp = new Span<char>(Array.Empty<char>());
            int idx = sp.LastIndexOf((char)0);
            Assert.Equal(-1, idx);
        }

        [Fact]
        public static void TestMatchLastIndexOf_Char()
        {
            for (int length = 0; length < 32; length++)
            {
                char[] a = new char[length];
                for (int i = 0; i < length; i++)
                {
                    a[i] = (char)(i + 1);
                }
                Span<char> span = new Span<char>(a);

                for (int targetIndex = 0; targetIndex < length; targetIndex++)
                {
                    char target = a[targetIndex];
                    int idx = span.LastIndexOf(target);
                    Assert.Equal(targetIndex, idx);
                }
            }
        }

        [Fact]
        public static void TestMultipleMatchLastIndexOf_Char()
        {
            for (int length = 2; length < 32; length++)
            {
                char[] a = new char[length];
                for (int i = 0; i < length; i++)
                {
                    a[i] = (char)(i + 1);
                }

                a[length - 1] = (char)200;
                a[length - 2] = (char)200;

                Span<char> span = new Span<char>(a);
                int idx = span.LastIndexOf((char)200);
                Assert.Equal(length - 1, idx);
            }
        }

        [Fact]
        public static void MakeSureNoChecksGoOutOfRangeLastIndexOf_Char()
        {
            for (int length = 0; length < 100; length++)
            {
                char[] a = new char[length + 2];
                a[0] = '9';
                a[length + 1] = '9';
                Span<char> span = new Span<char>(a, 1, length);
                int index = span.LastIndexOf('9');
                Assert.Equal(-1, index);
            }
        }
    }
}
