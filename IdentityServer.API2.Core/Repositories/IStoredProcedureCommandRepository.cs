using IdentityServer.API2.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.API2.Core.Repositories
{
    public interface IStoredProcedureCommandRepository
    {
        public Task SaveImageImageFile(ImageFile image);
        public Task SaveImageImageFileDetail(ImageFileDetail imageFileDetails);
    }
}
