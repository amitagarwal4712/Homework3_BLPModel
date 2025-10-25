using BLPModel.Model;

namespace BLPModel.Features
{
    public interface IBLPFunction
    {
        public SubjectModel addSubject(string pid, SecurityLevelEnum max_level, SecurityLevelEnum start_level);

        public ObjectModel addObject(string oid, SecurityLevelEnum level);

        public bool setLevel(string pid, SecurityLevelEnum new_level);

        public bool read(string pid, string oid);

        public bool write(string pid, string oid);

        public int level(string oid);

        public int current_level(string pid);

        public int max_level(string pid);
    }
}
