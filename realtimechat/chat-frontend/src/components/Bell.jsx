import { ReactComponent as BellSvg } from "./bell.svg";
import "./bell.scss";
import { Popover, Overlay } from "react-bootstrap";
import { useRef, useState } from "react";

const BellIcon = ({ alertCount, onClear, popOverBody}) => {
  const [show, setShow] = useState(false);
  const [target, setTarget] = useState(null);
  const ref = useRef(null);

  const handleClick = (event) => {
    setShow(!show);
    setTarget(event.target);
    onClear();
  };

  return (
    <div ref={ref}>

        <button className="bell" onClick={handleClick}>
          {alertCount > 0 ? <p>{alertCount}</p> : null}
          <BellSvg />
        </button>


      <Overlay
        show={show}
        target={target}
        placement="bottom"
        container={ref}
        containerPadding={20}
      >
        <Popover id="popover-contained">
          <Popover.Header as="h3">New Messages</Popover.Header>
          <Popover.Body>{popOverBody}</Popover.Body>
        </Popover>
      </Overlay>
    </div>
  );
};

export default BellIcon;
