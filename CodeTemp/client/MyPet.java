package client;


public class MyPet {

    public short id;
    public Pet template;
    public boolean isUse;
    public MyPet() {
    }

    public MyPet(short id, Pet template) {
        this.id = id;
        this.template = template;
        this.isUse = false;
    }
}
