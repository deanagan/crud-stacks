import React from "react";
import styled from "styled-components";

export const CardWrapper = styled.div`
  padding: 15px 25px;
  font-size: 12px;
  text-align: center;
  cursor: pointer;
  outline: none;
  color: white;
  background-color: #04aa6d;
  border: black solid 1em;
  border-radius: 5px;
  box-shadow: 0 3px #999;
  /* float: right; */
  margin-top: 2px;
  margin-left: 5px;
  margin-bottom: 10px;
  height: 80px;
  width: 10%;
`;

interface CardProp {
  totalIncompleteTasks: number;
  totalTasks: number;
}

export const StatCard: React.FC<CardProp> = ({
  totalIncompleteTasks,
  totalTasks,
}) => {
  return (
    <CardWrapper style={{ color: "#fff" }}>
      <h2>Summary</h2>

      <div>Incomplete Tasks: {totalIncompleteTasks}</div>
      <div>Total Tasks: {totalTasks}</div>
    </CardWrapper>
  );
};
