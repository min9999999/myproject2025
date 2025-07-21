using System;

class Program
{
    static void Main()
    {
        // 3x3 검사 결과 매트릭스(1:OK, 0: NG)
        int[,] inspectionResult = new int[,]
        {
            {1, 1, 0 },
            {1, 0, 1 },
            {1, 1, 1 }
        };

        // 결과 전체 출력 (디버깅용)
        Console.WriteLine("검사 결과 매트릭스");
        for (int row = 0; row < inspectionResult.GetLength(0); row++)
        {
            for (int col = 0; col < inspectionResult.GetLength(1); col++)
            {
                Console.WriteLine($"{inspectionResult[row, col]}");
            }
            Console.WriteLine();
        }

        // 특정 포인트 확인
        int result = inspectionResult[1, 2]; // 인덱스는 0부터
        string status = result == 1 ? "OK" : "NG";
        Console.WriteLine($"2행 3열 결과: {status}");

        Console.WriteLine("\n NG 위치 목록");
        for (int row = 0; row < inspectionResult.GetLength(0); row++)
        {
            for (int col = 0; col < inspectionResult.GetLength(1); col++)
            {
                if (inspectionResult[row, col] == 0)
                {
                    Console.WriteLine($"-> NG 발견: {row + 1}행 {col + 1}열 (index: [{row},{col}])");
                }
            }
        }
    }
}