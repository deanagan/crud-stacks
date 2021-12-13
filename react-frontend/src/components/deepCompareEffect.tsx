// The MIT License (MIT)
// Copyright (c) 2020 Kent C. Dodds

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
import { useEffect, useMemo, useRef } from 'react';
import { isEqual } from 'lodash';

type UseEffectParams = Parameters<typeof useEffect>
type EffectCallback = UseEffectParams[0]
type DependencyList = UseEffectParams[1]

function useDeepCompareMemoize<T>(value: T) {
    const ref = useRef<T>(value)
    const signalRef = useRef<number>(0)

    if (!isEqual(value, ref.current)) {
      ref.current = value
      signalRef.current += 1
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
    return useMemo(() => ref.current, [signalRef.current])
  }

export function useDeepCompareEffect(callback: EffectCallback, dependencies: DependencyList) {
    // eslint-disable-next-line react-hooks/exhaustive-deps
    return useEffect(callback, useDeepCompareMemoize(dependencies))
}