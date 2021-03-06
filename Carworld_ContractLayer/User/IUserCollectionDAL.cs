﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ContractLayer
{
    public interface IUserCollectionDAL
    {
        bool Create(UserDTO user);
        bool Delete(UserDTO user);
        List<UserDTO> GetAll();
        UserDTO Get(int userId);
        int GetId(UserDTO user);
    }
}
