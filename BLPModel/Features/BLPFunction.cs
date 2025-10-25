using BLPModel.Model;
using System.ComponentModel;
using System.Security.Cryptography;

namespace BLPModel.Features
{
    public class BLPFunction : IBLPFunction
    {
        private readonly List<SubjectModel> sModel;
        private readonly List<ObjectModel> oModel;
        public BLPFunction()
        {
            
        }
        public BLPFunction(List<SubjectModel> subjectModel, List<ObjectModel> objectModel)
        {
            this.sModel = subjectModel;
            this.oModel = objectModel;
        }
        public SubjectModel addSubject(string pid, SecurityLevelEnum max_level, SecurityLevelEnum start_level)
        {
            return new SubjectModel() { Pid = pid, Max_Level = max_level, Start_Level = start_level };

        }

        public ObjectModel addObject(string oid, SecurityLevelEnum level)
        {
            return new ObjectModel() { Oid = oid, Level = level };

        }

        public bool setLevel(string pid, SecurityLevelEnum new_level)
        {

            if (((int)new_level  > max_level(pid)) || ((int)new_level < current_level(pid))) {
                throw new Exception("New level can not excedd from allowed max level or lowering the crurrent level not allowed");
            }
            this.sModel.Find(m => m.Pid == pid).Start_Level = new_level;
            return true;
        }

        public bool read(string pid, string oid)
        {
            //if Lc(pid) <= Lc(oid)  read allowed
            if(level(oid) <= current_level(pid))
            {
                return true;
            }
            else if((level(oid) > current_level(pid)) && (level(oid) <= max_level(pid))){
                setLevel(pid, (SecurityLevelEnum)level(oid));
                return true;
            }
            return false;

        }

        public bool write(string pid, string oid)
        {
            if ((level(oid) >= current_level(pid)) && (level(oid) <= max_level(pid)))
            {
                setLevel(pid, (SecurityLevelEnum)level(oid));
                return true;
            }
                return false;
        }

        public int level(string oid) {
            return (int)oModel.Find(m => m.Oid == oid).Level;
        }

        public int current_level(string pid)
        {
            return (int)sModel.Find(m => m.Pid == pid).Start_Level;
        }

        public int max_level(string pid)
        {
            return (int)sModel.Find(m => m.Pid == pid).Max_Level;
        }
    }
}
