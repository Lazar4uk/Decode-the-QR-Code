using System;
using System.Collections.Generic;
using System.Text;

public class CodeWars {
  public static string Scanner(int[][] qrcode)
  {
      List<byte> bytes = new List<byte>();
      int x = 20, y = 20;
      var end = false;
      var dir = -1;
      while(!end)
      {
          var bit0 = (mask(x, y) ? 0 : 1 ) == qrcode[y][x];
          var bit1 = (mask(x - 1, y) ? 0 : 1) == qrcode[y][x - 1];
          bytes.Add((byte)(bit0 ? 1 : 0));
          bytes.Add((byte)(bit1 ? 1 : 0));
          y += dir;
          if ((y == 21 & x > 10) | (y == 8 & (x > 12 | x < 9)) | (y == 12 & x < 9) | y == -1)
          {
              dir = -dir;
              y += dir;
              x -= 2;
          }
          else if (y == 21 & x == 10)
          {
              x -= 2;
              y = 12;
              dir = -dir;
          }
          if (y == 6)
              y += dir;
          if (x == 6)
              x--;
          if (x == -1)
              end = true;
      }
      bytes.RemoveRange(0, 4);
      var N = bytes.Count;
      N = N - (N % 8);
      for (int i = 0; i < N; i++)
      {
          bytes[i] = (byte)(bytes[i] << (7 - (i & 7)));
          if (i % 8 != 0)
              bytes[i & (~7)] += bytes[i];
      }
      N = bytes[0];
      bytes.RemoveRange(0, 8);
      byte[] data = new byte[N];
      for (int i = 0; i < N;i ++)
      {
          data[i] = bytes[0];
          bytes.RemoveRange(0, 8);
      }
      return Encoding.ASCII.GetString(data);
  }
  
  public static bool mask(int x, int y) 
  {
      return (x + y)%2 == 0;
  }
}
