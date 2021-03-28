using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;
using Microsoft.SqlServer.Server;
using System.IO;


[Serializable]
[SqlUserDefinedType(Format.UserDefined,
    MaxByteSize = -1)]
public struct PhoneNumber: INullable, IBinarySerialize
{
    private string _number;

    public SqlString Number
    {
        get { return new SqlString(_number); }
        set
        {
            if(value == null)
            {
                _number = string.Empty;
                return;
            }

            string str = (string)value;

            if (Regex.IsMatch(str, @"^\+375(17|29|33|44|25)[0-9]{7}$"))
            {
                _number = str;
            }
            else
            {
                throw new ArgumentException("Phone number is not valid.");
            }
        }
    }

    public override string ToString()
    {
        return _number;
    }

    public string ToFormattedString()
    {
        return string.Format("{0} ({1}) {2}",
            _number.Substring(0, 4),
            _number.Substring(4, 2),
            _number.Substring(6, 7));
    }

    public bool IsNull
    {
        get { return string.IsNullOrEmpty(_number); }
    }

    public static PhoneNumber Null
    {
        get
        {
            PhoneNumber n = new PhoneNumber();
            n._number = string.Empty;
            return n;
        }
    }
   
    public static PhoneNumber Parse(SqlString s)
    {
        if (s.IsNull)
            return PhoneNumber.Null;

        PhoneNumber n = new PhoneNumber();
        n.Number = s;

        return n;
    }

    public void Read(BinaryReader r)
    {
        _number = r.ReadString();
    }

    public void Write(BinaryWriter w)
    {
        w.Write(_number);
    }
}