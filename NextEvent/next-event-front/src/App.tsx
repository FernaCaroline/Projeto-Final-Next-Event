import React from "react";
import CadastrarAdiministrador from './componentes/CadastrarAdiministrador';
import CadastrarParticipante from './componentes/CadastrarParticipante'
import CadastrarEvento from './componentes/CadastrarEvento';
import AtualizarEvento from './componentes/AtualizarEvento';
import BuscarEventoID from './componentes/BuscarEventoID';
import ListarEventos from './componentes/ListarEventos';
import RealizarInscricao from "./componentes/RealizarInscricacao";
import {BrowserRouter, Route, Routes, Link} from "react-router-dom";
//Ja deixei os imports preparados, para não correr o risco de esquecermos, lembrem que devem instalar as bibliotecas localmente.

function App() {
  return (
    <div>
      <h1>Inicio do FrontEnd do projeto NextEvent</h1>
    </div>
  );
}

export default App;
