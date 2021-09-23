using Xunit;
using DAL;
using Persistance;

namespace DALTest
{
    public class InvoiceDALTest
    {
        private LaptopDAL ldal = new LaptopDAL();

        [Theory]
        [InlineData(1)]

        private void GetInvoice(int _no)
        {
            InvoiceDAL idal = new InvoiceDAL();
            Invoice iv = idal.GetInvoiceById(new Invoice {InvoiceNo = _no});

            Assert.True(iv != null);
            Assert.True(iv.LaptopList.Count == 2);
            Assert.True(iv.InvoiceNo == _no);

        }

    }
}
