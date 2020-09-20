public class LoginController 
{
    LoginModel model;

    public LoginController()
    {
        model = new LoginModel();
    }

    public Login get(int id)
    {
        return model.get(id);
    }

    public Login logar(string usuario, string senha)
    {
        return model.get(usuario, senha);
    }

    public Login save(Login login)
    {
        return model.save(login);
    }

    public void setAtivo(int id)
    {
        model.setAtivo(id);
    }

    public Login getAtivo()
    {
        return model.getAtivo();
    }
}
