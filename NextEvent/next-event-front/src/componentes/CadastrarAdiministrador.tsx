import { useState } from "react";
import Administrador from "../interfaces/Adiministrador";
import axios from "axios";

function CadastrarAdministrador() {
    const [nome, setNome] = useState("");
    const [email, setEmail] = useState("");
    const [senha, setSenha] = useState("");

    function cadastrarAdministrador(e: any) {
        e.preventDefault();
        cadastrarAdministradorAPI();
    }

    async function cadastrarAdministradorAPI() {
        try {
            const administrador: Administrador = {
                nome,
                email,
                senha
            };

            const resposta = await axios.post(
                "http://localhost:5162/api/administrador/cadastrar",
                administrador
            );

            console.log(resposta.data);
            alert("Administrador cadastrado com sucesso!");
        } catch (error) {
            console.log("Erro no cadastro do administrador: " + error);
        }
    }

    return (
        <div>
            <h1>Cadastrar Administrador</h1>
            <form onSubmit={cadastrarAdministrador}>
                <div>
                    <label>Nome:</label>
                    <input type="text" onChange={(e) => setNome(e.target.value)} />
                </div>
                <div>
                    <label>Email:</label>
                    <input type="text" onChange={(e) => setEmail(e.target.value)} />
                </div>
                <div>
                    <label>Senha:</label>
                    <input type="password" onChange={(e) => setSenha(e.target.value)} />
                </div>
                <div>
                    <button type="submit">Cadastrar</button>
                </div>
            </form>
        </div>
    );
}

export default CadastrarAdministrador;
