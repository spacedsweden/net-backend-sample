namespace SinchBackend.Models {
    public class Svaml {
        public Instruction[] Instructions { get; set; }
        public Action Action { get; set; }
    }
    public class Instruction {
        public string Name { get; set; }
        public string Ids { get; set; }
        public string Locale { get; set; }

    }
    public class ICEAction:Action {
        public string Number { get; set; }
        public string locale { get; set; }
        public int maxDuration { get; set; }
        public bool callback { get; set; }
        public string cli { get; set; }
    }
    public class SimpleICEAction : Action {
        public int maxDuration { get; set; }
        public bool callback { get; set; }
    }

    public class Action {
        public string Name { get; set; }
    }
}