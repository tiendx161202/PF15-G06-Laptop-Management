// using System;
// using Xunit;
// using DAL;
// using Persistance;

// namespace DALTest
// {
//     public class LaptopDALTest
//     {
//         private LaptopDAL ldal = new LaptopDAL();
//         private const int FOUND = 1;
//         private const int NOT_FOUND = -1;
//         private const int EXCEPTION = -2;
//         private const int OUT_OF_STOCK = 0;

//         [Theory]
//         [InlineData(3, FOUND)]
//         [InlineData(2, FOUND)]
//         [InlineData(100, NOT_FOUND)]
//         [InlineData(-10, NOT_FOUND)]
//         [InlineData(50, NOT_FOUND)]
//         public void SearchIdTest(int id, int expected)
//         {
//             Laptop laptop1 = new Laptop() { LaptopId = id };
//             int result = ldal.GetLaptop(laptop1).Status;
//             Assert.True(expected == result);
//         }

//     }
// }
